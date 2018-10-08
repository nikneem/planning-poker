FROM microsoft/dotnet:2.1.5-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 57424
EXPOSE 44359

FROM microsoft/dotnet:2.1.5-aspnetcore-runtime AS build
WORKDIR /src
COPY ["HexMaster.PlanningPoker.Refinements/HexMaster.PlanningPoker.Refinements.csproj", "HexMaster.PlanningPoker.Refinements/"]
RUN dotnet restore "HexMaster.PlanningPoker.Refinements/HexMaster.PlanningPoker.Refinements.csproj"
COPY . .
WORKDIR "/src/HexMaster.PlanningPoker.Refinements"
RUN dotnet build "HexMaster.PlanningPoker.Refinements.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.PlanningPoker.Refinements.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.PlanningPoker.Refinements.dll"]