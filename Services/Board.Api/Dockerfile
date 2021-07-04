FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.csproj ./
RUN dotnet restore *.csproj

COPY . .
RUN dotnet build -c Release

COPY . ../
RUN dotnet publish -c release -o /app --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
RUN mkdir StaticFiles 
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "Board.Api.dll"]