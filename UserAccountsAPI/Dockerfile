FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["UserAccountsAPI/UserAccountsAPI.csproj", "UserAccountsAPI/"]
RUN dotnet restore "UserAccountsAPI/UserAccountsAPI.csproj"
COPY . .
WORKDIR "/src/UserAccountsAPI"
RUN dotnet build "UserAccountsAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserAccountsAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserAccountsAPI.dll"]