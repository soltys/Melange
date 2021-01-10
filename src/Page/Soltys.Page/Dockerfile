#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq

WORKDIR /src
COPY ["Soltys.Page.csproj", "Soltys.Page.csproj"]
RUN dotnet restore "Soltys.Page.csproj"
COPY . .
RUN dotnet build "Soltys.Page.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Soltys.Page.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Soltys.Page.dll"]
