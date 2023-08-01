#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 5100

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /source
COPY ["src/Services/AirPortService/AirPortService.csproj", "Services/AirPortService/"]
COPY ["src/Microservices.Gateway/Shared/Microservices.Gateway.Shared.csproj", "Microservices.Gateway/Shared/"]
RUN dotnet restore "Services/AirPortService/AirPortService.csproj"
COPY "src/Services/AirPortService/" "Services/AirPortService/"
WORKDIR "Services/AirPortService/"
RUN dotnet build "AirPortService.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "AirPortService.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AirPortService.dll"]