using System;
using Core.Implementers.Commands;
using Core.Interfaces;
using Core.Interfaces.Gateways.Repositories;
using Infastructure.Data.Mock;
using Infastructure.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace Infastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterLoggers(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, ConsoleLogger>();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IStreamConfigRepository, StreamConfigRepositoryMock>();
        }
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddTransient<ICommand, SQLToKafkaCommand>();
        }        
    }
}