FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs icu-data-full
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["TechnicalTestBravi.Api.csproj", "./"]
RUN dotnet restore "TechnicalTestBravi.Api.csproj"
COPY . .
WORKDIR "/src/."

RUN dotnet build "TechnicalTestBravi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechnicalTestBravi.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","TechnicalTestBravi.Api.dll"]