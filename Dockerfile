# ====== Build Stage ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ====== Runtime Stage ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TournamentManagementSystem.dll"]

#docker rm -f tms-api-container tms-postgres
