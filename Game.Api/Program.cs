using System.Text.Json.Serialization;
using Game.Api.Middlewares;
using Game.Application.Options;
using Game.Application.Services;
using Game.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Randomizer>(options => builder.Configuration.GetSection(nameof(Randomizer)).Bind(options));

builder.Services.AddDbContext<GameDbContext>(options => options.UseInMemoryDatabase("database"));
builder.Services.AddScoped<IRandomizerService, RandomizerService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using IServiceScope scope = app.Services.CreateScope();
GameDbContext context = scope.ServiceProvider.GetRequiredService<GameDbContext>();
context.Database.EnsureCreated();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();