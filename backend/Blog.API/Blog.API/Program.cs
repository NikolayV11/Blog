using Blog.DataAccess; // Твой неймспейс, где лежит BlogDbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Получаем строку подключения из appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Регистрируем контекст базы данных (используем Pomelo для MySQL)
builder.Services.AddDbContext<BlogDbContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// 3. Добавляем стандартные сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Чтобы ты мог тестировать API через браузер

var app = builder.Build();

// 4. Настраиваем Swagger (визуальный интерфейс для запросов)
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
