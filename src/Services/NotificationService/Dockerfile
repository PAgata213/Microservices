FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Services/NotificationService", "Services/NotificationService"]
COPY ["Microservices.Gateway/Shared", "Microservices.Gateway/Shared"]
RUN dotnet restore "Services/NotificationService/NotificationService.csproj"
RUN dotnet build "/src/Services/NotificationService/NotificationService.csproj" -c Release -o out

FROM build AS publish
RUN dotnet publish "/src/Services/NotificationService/NotificationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.dll"]