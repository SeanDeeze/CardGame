# escape=` 

 FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base

    WORKDIR /source

    # Prevent 'Warning: apt-key output should not be parsed (stdout is not a terminal)'
    ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1

    # install NodeJS 13.x
    # see https://github.com/nodesource/distributions/blob/master/README.md#deb
   RUN apt-get update -yq 
    RUN apt-get install curl gnupg -yq 
    RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
    RUN apt-get install -y nodejs

    COPY ./CardGameAPI/CardGameAPI/CardGameUI/package.json /source/package.json
    RUN npm install -g npm@7.8.0
    RUN ng update --all --force
    RUN npm install

    COPY ./CardGameAPI/CardGameAPI/CardGameUI/. /source/
    RUN npm run-script compile

    RUN find -type d -exec chmod +w {} +

# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

    LABEL maintainer='davidseandunn@gmail.com'

    WORKDIR /source

    # Copy csproj and restore as distinct layers
    COPY ./CardGameAPI/CardGameAPI/*.csproj ./
    RUN dotnet restore

    # Copy everything else and build
    COPY ./CardGameAPI/CardGameAPI/. ./

    # Delete obj and bin folders
    RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
    RUN find -type d -name CardGameUI -prune -exec rm -rf {} \;

    RUN dotnet publish -c Release -o cardgameapi

    RUN cp -r ./cardgameapi/. /cardgame/

    WORKDIR /cardgame/
    COPY --from=base /source/. ./CardGameUI/
    RUN rm web.config
    RUN ls

    WORKDIR /cardgame/CardGameUI/dist/
    RUN rm web.config
    RUN ls

    WORKDIR /cardgame/

ENTRYPOINT ["dotnet", "CardGame.dll"]
