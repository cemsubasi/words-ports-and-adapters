# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0.203-alpine3.18-amd64 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
# COPY Infra/*.csproj ./
RUN dotnet restore

# Copy everything else and build
RUN dotnet publish -c Release -r linux-x64 --self-contained -o out

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.3-alpine3.18-amd64 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Infra.dll"]
