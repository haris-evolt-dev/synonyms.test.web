FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Synonyms.Test.Web/Synonyms.Test.Web.csproj", "Synonyms.Test.Web/"]
COPY ["Synonyms.Test.Web.Models/Synonyms.Test.Web.Models.csproj", "Synonyms.Test.Web.Models/"]
COPY ["Synonyms.Test.Web.Services/Synonyms.Test.Web.Services.csproj", "Synonyms.Test.Web.Services/"]
RUN dotnet restore "Synonyms.Test.Web/Synonyms.Test.Web.csproj"
COPY . .
WORKDIR "/src/Synonyms.Test.Web"
RUN dotnet build "Synonyms.Test.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Synonyms.Test.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Synonyms.Test.Web.dll"]