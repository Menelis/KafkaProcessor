FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infastructure/Infastructure.csproj", "Infastructure/"]
COPY ["KafkaProducerClient/KafkaProducerClient.csproj", "KafkaProducerClient/"]
RUN dotnet restore "KafkaProducerClient/KafkaProducerClient.csproj"
COPY . .
WORKDIR "/src/KafkaProducerClient"
RUN dotnet build "KafkaProducerClient.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "KafkaProducerClient.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaProducerClient.dll"]

# docker build -t kafka-producer:latest .