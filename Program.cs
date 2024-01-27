
using aplicatieHandbal.Data;
using aplicatieHandbal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using aplicatieHandbal.Helpers;
using Serilog;
StaticLogger.EnsureInitialized();
Log.Information("Azure Storage API Booting Up...");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Azure Repository Service
builder.Services.AddTransient<IAzureStorage, AzureStorage>();
Log.Information("Services has been successfully added...");
builder.Services.AddDbContext<AplicatieDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FullStackConnectionString"));
});

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<IGameService, GameService>();


builder.Services.AddScoped<IStaffService, StaffService>();


builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
    = new DefaultContractResolver());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapControllers();
app.UseCors("AllowAll");
app.Run();