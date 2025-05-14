# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution and project files
COPY *.sln .
COPY */*.csproj ./

# Restore dependencies
RUN for file in *.csproj; do mkdir -p ${file%/*} && mv "$file" "${file%/*}/"; done && \
    dotnet restore

# Copy everything else
COPY . .

# Build the project
WORKDIR /src/SocialApp.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
# Set user only if created at runtime (avoid build-time issues)
# USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialApp.Api.dll"]
