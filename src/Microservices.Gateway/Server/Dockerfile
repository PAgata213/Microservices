FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Server/Microservices.Gateway.Server.csproj"
RUN dotnet build "/src/Server/Microservices.Gateway.Server.csproj" -c Release -o out

FROM build AS publish
RUN dotnet publish "/src/Server/Microservices.Gateway.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Gateway.Server.dll"]