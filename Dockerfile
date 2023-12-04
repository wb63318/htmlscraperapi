#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the entire project and build the application
COPY . .
RUN dotnet publish -c Release -o out

# Use the official ASP.NET runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the published application from the build image
COPY --from=build /app/out .

# Expose the port that your application listens on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "htmlscraperapi.dll"]
