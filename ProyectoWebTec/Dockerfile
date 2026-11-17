FROM mcr.microsoft.com/dotnet/sdk:10.0
WORKDIR /src

COPY ProyectoWebTec.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release -o /app/build

WORKDIR /app/build

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ProyectoWebTec.dll"]
