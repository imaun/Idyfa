FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["samples/Idyfa.Samples.BasicWeb/Idyfa.Samples.BasicWeb.csproj", "Idyfa.Samples.BasicWeb/"]
RUN dotnet restore "samples/Idyfa.Samples.BasicWeb/Idyfa.Samples.BasicWeb.csproj"
COPY . .
WORKDIR "/src/Idyfa.Samples.BasicWeb"
RUN dotnet build "Idyfa.Samples.BasicWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Idyfa.Samples.BasicWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Idyfa.Samples.BasicWeb.dll"]
