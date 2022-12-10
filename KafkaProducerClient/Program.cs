using Core.Extensions;
using Core.Implementers.Commands;
using Core.Interfaces;
using Core.Interfaces.Gateways.Repositories;
using Infastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    static IServiceProvider ServiceProvider;
    
    static void LoadConfigurationFile(IConfigurationBuilder builder, string environmentName, bool isProduction)
    {
        if(!isProduction && !string.IsNullOrEmpty(environmentName?.Trim()))
        {
            builder.AddJsonFile($"appsettings.{environmentName}.json", false, true);
            return;
        }
        builder.AddJsonFile("appsettings.json", false, true);
    }
    private static void Main(string[] args)
    {
        string environmentName = null;
        bool isProduction = false;
        IHostEnvironment HostEnvironment = null;
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, builder) =>
            {
                environmentName = context.HostingEnvironment.EnvironmentName;
                isProduction = context.HostingEnvironment.IsProduction();
                HostEnvironment = context.HostingEnvironment;
            }).Build();
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddEnvironmentVariables();
        LoadConfigurationFile(builder, environmentName, isProduction);
        IConfiguration configuration = builder.Build();
        var services = new ServiceCollection();
        services.RegisterCommands();
        services.RegisterLoggers();
        services.AddRepositories();

        ServiceProvider = services.BuildServiceProvider();


        IStreamConfigRepository streamConfigRepository = ServiceProvider.GetService<IStreamConfigRepository>();
        ILogger _logger = ServiceProvider.GetService<ILogger>();
        IConfigurationSection configurationSection = configuration.GetSection("ApplicationSettings");
        string workEnvironmentVariableName = configurationSection["WorkerEnvironmentVarableName"];
        string workerName = Environment.GetEnvironmentVariable(workEnvironmentVariableName);
        if(workerName.IsNullOrEmptyOrWhiteSpace())
        {
            Exception exception = new($"No environment variable:{workEnvironmentVariableName} was set for this worker");
            _logger.LogException(exception);
            return;
        }

        int runInEveryMinute = int.Parse(configurationSection["RunEveryMinute"]);
        int milliSeconds = runInEveryMinute * 60000;
        try
        {
            while(true)
            {
                _logger.Log($"Environment is :{environmentName}");
                _logger.Log($"Worker: {workerName} is about to sleep for {milliSeconds} milliseconds");
                Thread.Sleep(milliSeconds);
                _logger.Log($"Worker:{workerName} is starting");
                _logger.Log("Reading data from source");
                var items = streamConfigRepository.GetStreamConfigs();
                IEnumerable<ICommand> commands = ServiceProvider.GetServices<ICommand>();
                ICommand sqltoKafkaCommand = commands.FirstOrDefault(item => item.GetType() == typeof(SQLToKafkaCommand));
                if(sqltoKafkaCommand is null)
                {
                    _logger.Log($"No command was found of type:{nameof(SQLToKafkaCommand)}");
                    return;
                }
                foreach(var item in items)
                {
                    sqltoKafkaCommand.Execute(item);
                }
                _logger.Log($"Worker:{workerName} has completed");
            }
            
        }
        catch(Exception exception)
        {
            _logger.LogException(exception);
        }
        
    }
}