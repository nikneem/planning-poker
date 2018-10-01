FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 65084
EXPOSE 44378

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.ScrumPoker.Live/HexMaster.ScrumPoker.Live.csproj", "HexMaster.ScrumPoker.Live/"]
RUN dotnet restore "HexMaster.ScrumPoker.Live/HexMaster.ScrumPoker.Live.csproj"
COPY . .
WORKDIR "/src/HexMaster.ScrumPoker.Live"
RUN dotnet build "HexMaster.ScrumPoker.Live.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.ScrumPoker.Live.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.ScrumPoker.Live.dll"]