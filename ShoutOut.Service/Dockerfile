FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ShoutOut/ShoutOut.csproj", "ShoutOut/"]
RUN dotnet restore "ShoutOut/ShoutOut.csproj"
COPY . .
WORKDIR "/src/ShoutOut"
RUN dotnet build "ShoutOut.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ShoutOut.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ShoutOut.dll"]