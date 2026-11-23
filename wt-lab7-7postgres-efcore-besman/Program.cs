using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_besman.Data;
using wt_lab7_7postgres_efcore_besman.Models;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// 1. Получаем локальную строку подключения (для запуска на компьютере)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. ПРОВЕРКА ДЛЯ RENDER: Если есть переменная DATABASE_URL, используем её
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    
    connectionString = $"Host={uri.Host};" +
                       $"Port={uri.Port};" +
                       $"Database={uri.AbsolutePath.TrimStart('/')};" +
                       $"Username={userInfo[0]};" +
                       $"Password={userInfo[1]};" +
                       $"SSL Mode=Disable"; 
}

// 3. Регистрируем базу данных PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Настройка кодировки для русского языка
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

var app = builder.Build();

// 4. Автоматическое применение миграций при запуске
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ==========================================
// CRUD Эндпоинты и LINQ
// ==========================================

app.MapGet("/api/accounts", async (AppDbContext db) =>
{
    return await db.BankAccounts.ToListAsync();
});

app.MapGet("/api/accounts/search", async (string? search, AppDbContext db) =>
{
    var query = db.BankAccounts.AsQueryable();
    if (!string.IsNullOrWhiteSpace(search))
    {
        query = query.Where(x => x.OwnerName.ToLower().Contains(search.ToLower()) 
                              || x.AccountNumber.ToLower().Contains(search.ToLower()));
    }
    return await query.ToListAsync();
});

app.MapGet("/api/accounts/{id:int}", async (int id, AppDbContext db) =>
{
    var account = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
    return account is null ? Results.NotFound() : Results.Ok(account);
});

app.MapPost("/api/accounts", async (BankAccount account, AppDbContext db) =>
{
    db.BankAccounts.Add(account);
    await db.SaveChangesAsync();
    return Results.Created($"/api/accounts/{account.Id}", account);
});

app.MapPut("/api/accounts/{id:int}", async (int id, BankAccount updatedAccount, AppDbContext db) =>
{
    var account = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
    if (account is null) return Results.NotFound();

    account.AccountNumber = updatedAccount.AccountNumber;
    account.OwnerName = updatedAccount.OwnerName;
    account.Balance = updatedAccount.Balance;
    account.IsBlocked = updatedAccount.IsBlocked;
    account.CreatedAt = updatedAccount.CreatedAt;

    await db.SaveChangesAsync();
    return Results.Ok(account);
});

app.MapDelete("/api/accounts/{id:int}", async (int id, AppDbContext db) =>
{
    var account = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == id);
    if (account is null) return Results.NotFound();

    db.BankAccounts.Remove(account);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Дополнительные LINQ-запросы (Раздел 19)
app.MapGet("/api/accounts/filter", async (AppDbContext db) =>
{
    var accounts = await db.BankAccounts.Where(x => x.Balance > 1000).ToListAsync();
    return Results.Ok(accounts);
});

app.MapGet("/api/accounts/sort", async (AppDbContext db) =>
{
    var accounts = await db.BankAccounts.OrderBy(x => x.Balance).ToListAsync();
    return Results.Ok(accounts);
});

app.MapGet("/api/accounts/active", async (AppDbContext db) =>
{
    var accounts = await db.BankAccounts.Where(x => !x.IsBlocked).OrderBy(x => x.OwnerName).ToListAsync();
    return Results.Ok(accounts);
});

app.MapGet("/api/accounts/custom", async (AppDbContext db) =>
{
    var accounts = await db.BankAccounts
        .Where(x => x.AccountNumber.Contains("123"))
        .OrderByDescending(x => x.Balance)
        .ToListAsync();
    return Results.Ok(accounts);
});

// 5. Запуск приложения с поддержкой порта Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5030";
app.Run($"http://0.0.0.0:{port}");