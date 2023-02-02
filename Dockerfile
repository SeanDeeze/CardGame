# escape=` 
FROM node:latest AS build
WORKDIR /source

RUN npm install -g npm@latest

COPY ./CardGameAPI/CardGameAPI/CardGameUI/package.json /source/package.json
RUN npm install

COPY ./CardGameAPI/CardGameAPI/CardGameUI/. /source/
RUN npm run-script compile

# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

LABEL maintainer='davidseandunn@gmail.com'

WORKDIR /source

# Copy csproj and restore as distinct layers
COPY ./CardGameAPI/CardGameAPI/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./CardGameAPI/CardGameAPI/. ./
RUN dotnet publish -c Release -o cardgameapi

RUN cp -r ./cardgameapi/. /cardgame/

WORKDIR /cardgame/
COPY --from=build /source/. ./CardGameUI/

WORKDIR /cardgame/

ENTRYPOINT ["dotnet", "CardGame.dll"]
