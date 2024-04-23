#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UrlShorter.Public/UrlShorter.Public.csproj", "UrlShorter.Public/"]
COPY ["UrlShorter.Application/UrlShorter.Application.csproj", "UrlShorter.Application/"]
COPY ["UrlShorter.Domain/UrlShorter.Domain.csproj", "UrlShorter.Domain/"]
COPY ["UrlShorter.Infrastructure/UrlShorter.Infrastructure.csproj", "UrlShorter.Infrastructure/"]
RUN dotnet restore "./UrlShorter.Public/UrlShorter.Public.csproj"
COPY . .
WORKDIR "/src/UrlShorter.Public"
RUN dotnet build "./UrlShorter.Public.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UrlShorter.Public.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShorter.Public.dll"]