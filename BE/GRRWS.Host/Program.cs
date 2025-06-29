using GRRWS.Application.Common;
using GRRWS.Host.Starup;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add SignalR services
builder.Services.AddSignalR();

// Add services to the container.
CommonObject.Initialize(builder.Configuration);
builder.Services.AddLogging(logging =>
{
    logging.AddConsole(); // Log ra console
    logging.AddDebug();   // Log cho debug
});
builder.Services.AddDbContext<GRRWSContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("MyDb"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null))
       .EnableSensitiveDataLogging()
       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Aligns with AsNoTracking in repository
}, ServiceLifetime.Scoped);
builder.Services.AddMemoryCache();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve exact casing
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false; // Don’t ignore case when deserializing
        options.JsonSerializerOptions.DictionaryKeyPolicy = null; // Preserve dictionary keys
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true, // Ensures token is not expired
        ValidateIssuerSigningKey = true, // Validates the signature with the signing key
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero // Optional: Reduces time tolerance when validating token expiration
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
    options.AddPolicy("Guess", policy => policy.RequireRole("Guess"));
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.UseInlineDefinitionsForEnums();

    option.SwaggerDoc("v1", new OpenApiInfo { Title = "GRRWS API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            //policy.AllowAnyOrigin()
            policy.WithOrigins("http://localhost:8081", "exp://192.168.1.5:8081", "http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                .WithExposedHeaders("Authorization")
                .AllowCredentials();
            ;
        });


});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.RegisterServices();
builder.Services.AddHttpClient();
//builder.WebHost.UseUrls("http://0.0.0.0:5000");
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("App is starting...");
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error != null)
        {
            Console.WriteLine($" Unhandled Exception: {exceptionHandlerPathFeature.Error}");
        }
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred.");
    });
});

app.UseCors("AllowAll");

// Map SignalR hub
app.MapHub<RequestHub>("/hubs/request");


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MigrateDatabases();
//app.ConfigureMiddleware();
app.MapControllers();


app.Run();

//re-push original