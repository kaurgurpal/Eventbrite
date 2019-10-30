FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
EXPOSE 80

COPY . .
RUN dotnet restore OrderAPI.csproj

FROM build AS publish
RUN dotnet publish "OrderAPI.csproj" -c Release -o /app


WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "OrderAPI.dll"]