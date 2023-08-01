#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 5001
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /source
COPY ["src/Microservices.Gateway/Server/Microservices.Gateway.Server.csproj", "Microservices.Gateway/Server/"]
COPY ["src/Microservices.Gateway/Client/Microservices.Gateway.Client.csproj", "Microservices.Gateway/Client/"]
COPY ["src/Microservices.Gateway/Shared/Microservices.Gateway.Shared.csproj", "Microservices.Gateway/Shared/"]
RUN dotnet restore "Microservices.Gateway/Server/Microservices.Gateway.Server.csproj"
COPY ["src/Microservices.Gateway/", "Microservices.Gateway/"]
WORKDIR "Microservices.Gateway/Server/"
RUN dotnet build "Microservices.Gateway.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Gateway.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Gateway.Server.dll"]