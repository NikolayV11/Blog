# 1. Базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. Образ для сборки (SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файлы всех проектов (важно для восстановления зависимостей)
COPY ["Blog.API/Blog.API.csproj", "Blog.API/"]
COPY ["Blog.Application/Blog.Application.csproj", "Blog.Application/"]
COPY ["Blog.Core/Blog.Core.csproj", "Blog.Core/"]
COPY ["Blog.DataAccess/Blog.DataAccess.csproj", "Blog.DataAccess/"]

# Восстанавливаем зависимости
RUN dotnet restore "Blog.API/Blog.API.csproj"

# Копируем всё остальное
COPY . .
WORKDIR "/src/Blog.API"

# Собираем проект
RUN dotnet build "Blog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Публикация
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Blog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Финальный этап
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Создаем папку для картинок внутри контейнера
RUN mkdir -p wwwroot/uploads

ENTRYPOINT ["dotnet", "Blog.API.dll"]
