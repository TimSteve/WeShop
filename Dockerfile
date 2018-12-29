FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY publish  .
HEALTHCHECK --interval=5s --timeout=20s \
    CMD curl -fs localhost/working || exit 1
ENTRYPOINT ["dotnet", "WeShop.WebApi.dll"]