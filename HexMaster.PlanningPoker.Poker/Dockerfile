FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 54925
EXPOSE 44389

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.PlanningPoker.Poker/HexMaster.PlanningPoker.Poker.csproj", "HexMaster.PlanningPoker.Poker/"]
RUN dotnet restore "HexMaster.PlanningPoker.Poker/HexMaster.PlanningPoker.Poker.csproj"
COPY . .
WORKDIR "/src/HexMaster.PlanningPoker.Poker"
RUN dotnet build "HexMaster.PlanningPoker.Poker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.PlanningPoker.Poker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.PlanningPoker.Poker.dll"]