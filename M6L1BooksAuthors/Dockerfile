FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY M6L1BooksAuthors.sln ./
COPY M6L1BooksAuthors/*.csproj ./M6L1BooksAuthors/
COPY M6L1BooksAuthors.Core/*.csproj ./M6L1BooksAuthors.Core/
COPY M6L1BooksAuthors.Infrastructure/*.csproj ./M6L1BooksAuthors.Infrastructure/

RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish M6L1BooksAuthors.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

ENV ASPNETCORE_URLS=http://*:8182
ENV ASPNETCORE_ENVIRONMENT=Development


WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 8182
ENTRYPOINT ["dotnet", "M6L1BooksAuthors.dll"]