#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ChatWebAPI/ChatWebAPI.csproj", "ChatWebAPI/"]
COPY ["Domain.Core/Domain.Core.csproj", "Domain.Core/"]
COPY ["Shared.Application/Shared.Application.csproj", "Shared.Application/"]
COPY ["Shared.WebAPI/Shared.WebAPI.csproj", "Shared.WebAPI/"]
RUN dotnet restore "./ChatWebAPI/ChatWebAPI.csproj"
COPY . .
WORKDIR "/src/ChatWebAPI"
RUN dotnet build "./ChatWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ChatWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatWebAPI.dll"]