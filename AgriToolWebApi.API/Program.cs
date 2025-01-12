using AgriToolWebApi.Application.Exceptions;
using AgriToolWebApi.Common.Extensions;
using AgriToolWebApi.Common.Settings;
using AgriToolWebApi.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ロギングの設定をクリアしてコンソールロギングを追加
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// LoggerFactory を一時的に作成
using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

// ロガーを取得
var logger = loggerFactory.CreateLogger<Program>();

// appsettings.jsonからデフォルトのDB接続文字列を取得
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var mySqlVersionString = builder.Configuration.GetValue<string>("DatabaseOptions:MySqlVersion");

Version mySqlVersion;

try
{
    mySqlVersion = Version.Parse(mySqlVersionString);
}
catch (FormatException ex)
{
    logger.LogError(ex, "MySQLバージョンのフォーマットが正しくありません");
    throw;
}

var serverVersion = new MySqlServerVersion(mySqlVersion);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, serverVersion));

// Application層のサービスの登録
builder.Services.AddApplicationServices();
// Common層のサービスの登録
builder.Services.AddCommonUtilities();

builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection("SecuritySettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
