# ===============================
# Build stage
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and restore
COPY *.sln .
COPY SliceCloud.Web/*.csproj SliceCloud.Web/
COPY SliceCloud.Service/*.csproj SliceCloud.Service/
COPY SliceCloud.Repository/*.csproj SliceCloud.Repository/

RUN dotnet restore

# Copy everything and build
COPY . .
WORKDIR /app/SliceCloud.Web
RUN dotnet publish -c Release -o /app/publish

# ===============================
# Runtime stage
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SliceCloud.Web.dll"]
