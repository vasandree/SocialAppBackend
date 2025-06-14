FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SocialApp.Api/SocialApp.Api.csproj", "SocialApp.Api/"]
COPY ["Auth.Application/Auth.Application.csproj", "Auth.Application/"]
COPY ["Auth.Contracts/Auth.Contracts.csproj", "Auth.Contracts/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["User.Contracts/User.Contracts.csproj", "User.Contracts/"]
COPY ["User.Domain/User.Domain.csproj", "User.Domain/"]
COPY ["Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["Shared.Contracts/Shared.Contracts.csproj", "Shared.Contracts/"]
COPY ["Auth.Infrastructure/Auth.Infrastructure.csproj", "Auth.Infrastructure/"]
COPY ["SocialNetworkAccounts.Contracts/SocialNetworkAccounts.Contracts.csproj", "SocialNetworkAccounts.Contracts/"]
COPY ["SocialNetworkAccounts.Domain/SocialNetworkAccounts.Domain.csproj", "SocialNetworkAccounts.Domain/"]
COPY ["Event.Infrastructure/Event.Infrastructure.csproj", "Event.Infrastructure/"]
COPY ["Event.Domain/Event.Domain.csproj", "Event.Domain/"]
COPY ["Shared.Persistence/Shared.Persistence.csproj", "Shared.Persistence/"]
COPY ["User.Controllers/User.Controllers.csproj", "User.Controllers/"]
COPY ["User.Application/User.Application.csproj", "User.Application/"]
COPY ["User.Persistence/User.Persistence.csproj", "User.Persistence/"]
COPY ["User.Infrastructure/User.Infrastructure.csproj", "User.Infrastructure/"]
COPY ["SocialNode.Contracts/SocialNode.Contracts.csproj", "SocialNode.Contracts/"]
COPY ["SocialNode.Domain/SocialNode.Domain.csproj", "SocialNode.Domain/"]
COPY ["Shared.Extensions/Shared.Extensions.csproj", "Shared.Extensions/"]
COPY ["SocialNetworkAccounts.Controllers/SocialNetworkAccounts.Controllers.csproj", "SocialNetworkAccounts.Controllers/"]
COPY ["SocialNetworkAccounts.Persistence/SocialNetworkAccounts.Persistence.csproj", "SocialNetworkAccounts.Persistence/"]
COPY ["SocialNetworkAccounts.Infrastructure/SocialNetworkAccounts.Infrastructure.csproj", "SocialNetworkAccounts.Infrastructure/"]
COPY ["SocialNetworkAccounts.Application/SocialNetworkAccounts.Application.csproj", "SocialNetworkAccounts.Application/"]
COPY ["SocialNode.Controllers/SocialNode.Controllers.csproj", "SocialNode.Controllers/"]
COPY ["SocialNode.Application/SocialNode.Application.csproj", "SocialNode.Application/"]
COPY ["SocialNode.Infrastructure/SocialNode.Infrastructure.csproj", "SocialNode.Infrastructure/"]
COPY ["SocialNode.Persistence/SocialNode.Persistence.csproj", "SocialNode.Persistence/"]
COPY ["Auth.Controllers/Auth.Controllers.csproj", "Auth.Controllers/"]
COPY ["Auth.Persistence/Auth.Persistence.csproj", "Auth.Persistence/"]
COPY ["TaskModule.Controllers/TaskModule.Controllers.csproj", "TaskModule.Controllers/"]
COPY ["TaskModule.Infrastructure/TaskModule.Infrastructure.csproj", "TaskModule.Infrastructure/"]
COPY ["TaskModule.Domain/TaskModule.Domain.csproj", "TaskModule.Domain/"]
COPY ["TaskModule.Persistence/TaskModule.Persistence.csproj", "TaskModule.Persistence/"]
COPY ["TaskModule.Contracts/TaskModule.Contracts.csproj", "TaskModule.Contracts/"]
COPY ["TaskModule.Application/TaskModule.Application.csproj", "TaskModule.Application/"]
COPY ["Event.Persistence/Event.Persistence.csproj", "Event.Persistence/"]
COPY ["Event.Contracts/Event.Contracts.csproj", "Event.Contracts/"]
COPY ["Event.Application/Event.Application.csproj", "Event.Application/"]
COPY ["Event.Controllers/Event.Controllers.csproj", "Event.Controllers/"]
COPY ["Workspace.Persistence/Workspace.Persistence.csproj", "Workspace.Persistence/"]
COPY ["Workspace.Contracts/Workspace.Contracts.csproj", "Workspace.Contracts/"]
COPY ["Workspace.Domain/Workspace.Domain.csproj", "Workspace.Domain/"]
COPY ["Workspace.Infrastructure/Workspace.Infrastructure.csproj", "Workspace.Infrastructure/"]
COPY ["Workspace.Controllers/Workspace.Controllers.csproj", "Workspace.Controllers/"]
RUN dotnet restore "SocialApp.Api/SocialApp.Api.csproj"
COPY . .
WORKDIR "/src/SocialApp.Api"
RUN dotnet build "SocialApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SocialApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialApp.Api.dll"]
