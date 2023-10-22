# https://hub.docker.com/_/node
FROM node:latest AS build
WORKDIR /source

RUN node -v

RUN npm install -g npm@latest

COPY ./CardGameAPI/CardGameAPI/CardGameUI/package.json /source/package.json
RUN npm install

COPY ./CardGameAPI/CardGameAPI/CardGameUI/. /source/
RUN npm run-script compile

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
LABEL maintainer='davidseandunn@gmail.com'
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY ./CardGameAPI/CardGameAPI/*.csproj /source/
RUN dotnet restore

# Copy everything else and build
COPY ./CardGameAPI/CardGameAPI/. /source/
RUN dotnet publish -c Release -o /source/cardgameapi

# Create new folder and copy dotnet compiled application
RUN cp -r /source/cardgameapi/. /cardgame/

# Now copy compiled angular application
WORKDIR /cardgame
COPY --from=build /source/. /cardgame/CardGameUI/

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /cardgame/. /app

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "CardGame.dll"]
