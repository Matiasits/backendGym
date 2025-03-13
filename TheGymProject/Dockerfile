# Etapa 1: build de la app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiamos el contenido del proyecto
COPY . ./

# Publicamos la app en modo Release
RUN dotnet publish -c Release -o out

# Etapa 2: imagen más liviana para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copiamos el resultado del build
COPY --from=build /app/out .

# Exponemos el puerto que usará la app en Render
EXPOSE 80

# Comando para iniciar la app
ENTRYPOINT ["dotnet", "TheGymProject.dll"]
