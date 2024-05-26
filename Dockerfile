# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine-amd64 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
# COPY nuget.config ./
COPY . ./

ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

# RUN dotnet restore 

# Copy everything else and build
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine-amd64 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "Infra.dll"]
