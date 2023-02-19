FROM node:latest AS build
WORKDIR /source

RUN npm install -g npm@latest

COPY ./CardGameAPI/CardGameAPI/CardGameUI/package.json /source/package.json
RUN npm install

COPY ./CardGameAPI/CardGameAPI/CardGameUI/. /source/
RUN npm run-script compile

RUN ls

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

# Create Root level Folder to combine UI and C-Sharp Compiled Applications
RUN cp -r ./cardgameapi/. /cardgame/

WORKDIR /cardgame/
COPY --from=build /source/. ./CardGameUI/

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /cardgame/ ./

RUN ls ./CardGameUI
RUN ls
RUN cat appsettings.json

ENV ASPNETCORE_URLS=http://+:80/
EXPOSE 80

ENTRYPOINT ["dotnet", "CardGame.dll"]
