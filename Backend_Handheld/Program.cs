using Backend_Handheld.Repositories.Interfaces;
using Backend_Handheld.Repositories;
using Backend_Handheld.Services.Interfaces;
using Backend_Handheld.Services;
using Dapper;
using Backend_Handheld.PostgreSqlHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
    option.AddPolicy("policy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddSingleton<IServiceManager, ServiceManager>();
builder.Services.AddSingleton<IRepositoryManager, RepositoryManager>();

DefaultTypeMap.MatchNamesWithUnderscores = true;
SqlMapper.AddTypeHandler(new DateTimeHandler());

var app = builder.Build();
app.UseCors("policy");
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
