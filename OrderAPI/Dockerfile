FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["OrderAPI/OrderAPI.csproj", "OrderAPI/"]
RUN dotnet restore "OrderAPI/OrderAPI.csproj"
COPY . .
WORKDIR "/src/OrderAPI"
RUN dotnet build "OrderAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OrderAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OrderAPI.dll"]