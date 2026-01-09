# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG AWS_ACCOUNT_ID
ARG AWS_REGION
ARG BASE_RUNTIME_IMAGE=${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/aspnet-newrelic-runtime:latest

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FCG.User.API/FCG.User.API.csproj", "FCG.User.API/"]
COPY ["FCG.User.Application/FCG.User.Application.csproj", "FCG.User.Application/"]
COPY ["FCG.User.Domain/FCG.User.Domain.csproj", "FCG.User.Domain/"]
COPY ["FCG.User.Infra.Data/FCG.User.Infra.Data.csproj", "FCG.User.Infra.Data/"]
RUN dotnet restore "./FCG.User.API/FCG.User.API.csproj"
COPY . .
WORKDIR "/src/FCG.User.API"
RUN dotnet build "./FCG.User.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.User.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# Stage final: usa a imagem do ECR com aspnet + new relic
FROM ${BASE_RUNTIME_IMAGE} AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.User.API.dll"]