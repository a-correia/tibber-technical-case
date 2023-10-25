FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

ENV DATABASE_CONNECTION='Host=postgres;Port=5432;Username=tibber;Password=tibber;Database=tibber'

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CleaningService.Api/CleaningService.Api.csproj", "CleaningService.Api/"]
COPY ["CleaningService.Data/CleaningService.Data.csproj", "CleaningService.Data/"]
COPY ["CleaningService.Services/CleaningService.Services.csproj", "CleaningService.Services/"]
COPY ["CleaningService.Domain.Model/CleaningService.Domain.Model.csproj", "CleaningService.Domain.Model/"]
RUN dotnet restore "CleaningService.Api/CleaningService.Api.csproj"
COPY . .
WORKDIR "/src/CleaningService.Api"
RUN dotnet build "CleaningService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleaningService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleaningService.Api.dll"]
