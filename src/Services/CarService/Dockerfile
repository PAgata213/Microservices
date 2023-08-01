FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Services/CarService", "Services/CarService"]
COPY ["Microservices.Gateway/Shared", "Microservices.Gateway/Shared"]
run ls -a
RUN dotnet restore "Services/CarService/CarService.csproj"
RUN dotnet build "/src/Services/CarService/CarService.csproj" -c Release -o out

FROM build AS publish
RUN dotnet publish "/src/Services/CarService/CarService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarService.dll"]