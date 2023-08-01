FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Services/HotelService", "Services/HotelService"]
COPY ["Microservices.Gateway/Shared", "Microservices.Gateway/Shared"]
run ls -a
RUN dotnet restore "Services/HotelService/HotelService.csproj"
RUN dotnet build "/src/Services/HotelService/HotelService.csproj" -c Release -o out

FROM build AS publish
RUN dotnet publish "/src/Services/HotelService/HotelService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelService.dll"]