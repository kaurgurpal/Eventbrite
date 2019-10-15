FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["WishListApi/WishListApi.csproj", "WishListApi/"]
RUN dotnet restore "WishListApi/WishListApi.csproj"
COPY . .
WORKDIR "/src/WishListApi"
RUN dotnet build "WishListApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WishListApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WishListApi.dll"]