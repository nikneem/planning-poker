FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 54113
EXPOSE 44324

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["HexMaster.PlanningPoker.Chat/HexMaster.PlanningPoker.Chat.csproj", "HexMaster.PlanningPoker.Chat/"]
RUN dotnet restore "HexMaster.PlanningPoker.Chat/HexMaster.PlanningPoker.Chat.csproj"
COPY . .
WORKDIR "/src/HexMaster.PlanningPoker.Chat"
RUN dotnet build "HexMaster.PlanningPoker.Chat.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HexMaster.PlanningPoker.Chat.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HexMaster.PlanningPoker.Chat.dll"]