FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/WeShop.WebApi/WeShop.WebApi.csproj", "src/WeShop.WebApi/"]
COPY ["src/WeShop.WebApi/WeShop.Bootstrapper.csproj", "src/WeShop.Bootstrapper/"]
COPY ["src/WeShop.Queries/WeShop.Queries.csproj", "src/WeShop.Queries/"]
COPY ["src/WeShop.Domain/WeShop.Domain.csproj", "src/WeShop.Domain/"]
COPY ["src/WeShop.Domain.Abstract/WeShop.Domain.Abstract.csproj", "src/WeShop.Domain.Abstract/"]
COPY ["src/WeShop.Infrastructure.Common/WeShop.Infrastructure.Common.csproj", "src/WeShop.Infrastructure.Common/"]
COPY ["src/WeShop.Infrastructure.Data/WeShop.Infrastructure.Data.csproj", "src/WeShop.Infrastructure.Data/"]
COPY ["src/WeShop.Infrastructure.Repositories/WeShop.Infrastructure.Repositories.csproj", "src/WeShop.Infrastructure.Repositories/"]
RUN dotnet restore "src/WeShop.WebApi/WeShop.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WeShop.WebApi"
RUN dotnet build "WeShop.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WeShop.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
HEALTHCHECK --interval=5s --timeout=20s \
    CMD curl -fs localhost/healthz || exit 1
ENTRYPOINT ["dotnet", "WeShop.WebApi.dll"]