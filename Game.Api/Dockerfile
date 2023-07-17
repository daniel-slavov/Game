FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Game.Api/Game.Api.csproj", "Game.Api/"]
COPY ["Game.Application/Game.Application.csproj", "Game.Application/"]
COPY ["Game.Data/Game.Data.csproj", "Game.Data/"]
RUN dotnet restore "Game.Api/Game.Api.csproj"
COPY . .
WORKDIR "/src/Game.Api"
RUN dotnet build "Game.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Game.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://*:5000

ENTRYPOINT ["dotnet", "Game.Api.dll"]
