# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY ResidentialSchoolManagementSystem.sln ./
COPY RSMS.Api/RSMS.Api.csproj RSMS.Api/
COPY RSMS.Business/RSMS.Business.csproj RSMS.Business/
COPY RSMS.Common/RSMS.Common.csproj RSMS.Common/
COPY RSMS.Data/RSMS.Data.csproj RSMS.Data/
COPY RSMS.Services/RSMS.Services.csproj RSMS.Services/
COPY RSMS.Tests/RSMS.Tests.csproj RSMS.Tests/

# Restore dependencies
RUN dotnet restore ResidentialSchoolManagementSystem.sln

# Copy the rest of the source code
COPY . .

# Publish the API project
RUN dotnet publish RSMS.Api/RSMS.Api.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Run the application
ENTRYPOINT ["dotnet", "RSMS.Api.dll"]
