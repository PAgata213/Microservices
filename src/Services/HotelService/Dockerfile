#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 5200

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /source
COPY ["src/Services/HotelService/HotelService.csproj", "Services/HotelService/"]
RUN dotnet restore "Services/HotelService/HotelService.csproj"
COPY "src/Services/HotelService" "Services/HotelService/"
WORKDIR "Services/HotelService"
RUN dotnet build "HotelService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelService.dll"]