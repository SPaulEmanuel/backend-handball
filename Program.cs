
using aplicatieHandbal.Data;
using aplicatieHandbal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using aplicatieHandbal.Helpers;
using Serilog;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Text;
StaticLogger.EnsureInitialized();
Log.Information("Azure Storage API Booting Up...");

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.Administrator, policy =>
    policy.RequireAssertion(context => context.User.Claims.First(x => x.Type == "Role").Value == Policies.Administrator));

    options.AddPolicy(Policies.CreatorDeContinut, policy =>
    policy.RequireAssertion(context => context.User.Claims.First(x => x.Type == "Role").Value == Policies.CreatorDeContinut));
});

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
app.UseMiddleware<JwtMiddleware>();

app.UseCors("AllowAll");
app.Run();