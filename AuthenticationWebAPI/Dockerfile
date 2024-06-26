#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthenticationWebAPI/AuthenticationWebAPI.csproj", "AuthenticationWebAPI/"]
COPY ["JwtAuthenticationManager/JwtAuthenticationManager.csproj", "JwtAuthenticationManager/"]
COPY ["Shared.Application/Shared.Application.csproj", "Shared.Application/"]
COPY ["Domain.Core/Domain.Core.csproj", "Domain.Core/"]
COPY ["Shared.WebAPI/Shared.WebAPI.csproj", "Shared.WebAPI/"]
RUN dotnet restore "./AuthenticationWebAPI/AuthenticationWebAPI.csproj"
COPY . .
WORKDIR "/src/AuthenticationWebAPI"
RUN dotnet build "./AuthenticationWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthenticationWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationWebAPI.dll"]