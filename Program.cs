using aplicatieHandbal.Data;
using aplicatieHandbal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AplicatieDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FullStackConnectionString"));
});
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapControllers();

app.Run();
