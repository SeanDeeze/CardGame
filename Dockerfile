# escape=` 
FROM node:latest AS build
    WORKDIR /source

    COPY ./CardGameAPI/CardGameAPI/CardGameUI/package.json /source/package.json
    RUN npm install --force

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

    # Delete obj and bin folders
    #RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
    #RUN find -type d -name CardGameUI -prune -exec rm -rf {} \;

    RUN dotnet publish -c Release -o cardgameapi

    RUN cp -r ./cardgameapi/. /cardgame/

    WORKDIR /cardgame/
    COPY --from=base /source/. ./CardGameUI/

    WORKDIR /cardgame/

ENTRYPOINT ["dotnet", "CardGame.dll"]
