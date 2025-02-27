# Use official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY ["OrderPlatform.sln", "./"]
COPY ["Order.Api/*.csproj", "Order.Api/"]
COPY ["Order.Common/*.csproj", "Order.Common/"]
COPY ["Order.Data/*.csproj", "Order.Data/"]
COPY ["Order.Mapping/*.csproj", "Order.Mapping/"]
COPY ["Order.Models/*.csproj", "Order.Models/"]
COPY ["Order.Repositories/*.csproj", "Order.Repositories/"]
COPY ["Order.Api.Tests/*.csproj", "Order.Api.Tests/"]
COPY ["Order.Mapping.Tests/*.csproj", "Order.Mapping.Tests/"]
COPY ["Order.Repositories.Tests/*.csproj", "Order.Repositories.Tests/"]
# Repeat for additional project folders as needed

RUN dotnet restore "OrderPlatform.sln"

# Copy the rest of the application files
COPY . .

RUN dotnet publish "Order.Api/Order.Api.csproj"  -c Release -o /app/publish


# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish . 
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "Order.Api.dll"]

