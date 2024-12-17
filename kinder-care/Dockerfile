# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["kinder-care.csproj", "./"]
RUN dotnet restore "kinder-care.csproj"

# Copy all files and build the project
COPY . .
RUN dotnet publish "kinder-care.csproj" -c Release -o /app

# Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Expose the port and set the entry point
EXPOSE 80
ENTRYPOINT ["dotnet", "kinder-care.dll"]
