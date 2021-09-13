FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ChalkBot/ChalkBot.csproj ChalkBot/
COPY ChalkBot/NuGet.Config .
RUN dotnet restore "ChalkBot/ChalkBot.csproj"
COPY . .
WORKDIR "/src/ChalkBot"
RUN dotnet build "ChalkBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChalkBot.csproj" -c Release -o /app/publish

FROM base AS final
COPY config.json /app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChalkBot.dll"]
