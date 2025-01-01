using AgriToolWebApi.Application.Interfaces;
using AgriToolWebApi.Application.Services;
using AgriToolWebApi.Common.Interfaces.Security;
using AgriToolWebApi.Common.Interfaces.Uuid;
using AgriToolWebApi.Common.Utilities.Security;
using AgriToolWebApi.Common.Utilities.Uuid;
using AgriToolWebApi.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json����f�t�H���g��DB�ڑ���������擾
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var mySqlVersion = builder.Configuration.GetSection("DatabaseOptions:MySqlVersion").Value;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(mySqlVersion))));

#region �T�[�r�X�w�̓o�^
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region ���[�e�B���e�B�̓o�^
builder.Services.AddScoped<IUuidGenerator, UuidGenerator>();
builder.Services.AddScoped<ISaltGenerator, SaltGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
#endregion

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
