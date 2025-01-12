using AgriToolWebApi.Application.Exceptions;
using AgriToolWebApi.Common.Extensions;
using AgriToolWebApi.Common.Settings;
using AgriToolWebApi.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ���M���O�̐ݒ���N���A���ăR���\�[�����M���O��ǉ�
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// LoggerFactory ���ꎞ�I�ɍ쐬
using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

// ���K�[���擾
var logger = loggerFactory.CreateLogger<Program>();

// appsettings.json����f�t�H���g��DB�ڑ���������擾
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var mySqlVersionString = builder.Configuration.GetValue<string>("DatabaseOptions:MySqlVersion");

Version mySqlVersion;

try
{
    mySqlVersion = Version.Parse(mySqlVersionString);
}
catch (FormatException ex)
{
    logger.LogError(ex, "MySQL�o�[�W�����̃t�H�[�}�b�g������������܂���");
    throw;
}

var serverVersion = new MySqlServerVersion(mySqlVersion);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, serverVersion));

// Application�w�̃T�[�r�X�̓o�^
builder.Services.AddApplicationServices();
// Common�w�̃T�[�r�X�̓o�^
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
