FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER 1654
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .

RUN dotnet restore "src/Services/Transaction/Transaction.API/Transaction.API.csproj"

RUN dotnet publish "src/Services/Transaction/Transaction.API/Transaction.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false \
    --no-restore

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Transaction.API.dll"]