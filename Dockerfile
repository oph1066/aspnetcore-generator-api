FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
#FROM microsoft/aspnetcore-build:2 AS build-env

WORKDIR /generator

COPY api/api.csproj ./api/
RUN dotnet restore api/api.csproj

COPY tests/tests.csproj ./tests/
RUN dotnet restore tests/tests.csproj

COPY . .
RUN dotnet test tests/tests.csproj

RUN dotnet publish api/api.csproj -o /publish


FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS run-env
COPY  --from=build-env /publish /publish

WORKDIR /publish/
ENTRYPOINT [ "dotnet","api.dll" ]
