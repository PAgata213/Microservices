FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS publish
COPY . /src
RUN dotnet publish "/src/Server/Microservices.Gateway.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Gateway.Server.dll"]