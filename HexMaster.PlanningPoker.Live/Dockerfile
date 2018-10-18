FROM microsoft/dotnet:2.1.5-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 57505
EXPOSE 44384

FROM microsoft/dotnet:2.1.5-sdk AS build
WORKDIR /src
COPY ["HexMaster.PlanningPoker.Live/HexMaster.PlanningPoker.Live.csproj", "HexMaster.PlanningPoker.Live/"]
RUN dotnet restore "HexMaster.PlanningPoker.Live/HexMaster.PlanningPoker.Live.csproj"
COPY . .
WORKDIR "/src/HexMaster.PlanningPoker.Live"
RUN dotnet build "HexMaster.PlanningPoker.Live.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.PlanningPoker.Live.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.PlanningPoker.Live.dll"]