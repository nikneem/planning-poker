FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.PlanningPoker.Spa/HexMaster.PlanningPoker.Spa.csproj", "HexMaster.PlanningPoker.Spa/"]
RUN dotnet restore "HexMaster.PlanningPoker.Spa/HexMaster.PlanningPoker.Spa.csproj"
COPY . .
WORKDIR "/src/HexMaster.PlanningPoker.Spa"
RUN dotnet build "HexMaster.PlanningPoker.Spa.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.PlanningPoker.Spa.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.PlanningPoker.Spa.dll"]
