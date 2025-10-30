using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RSMS.Common.Models;
using RSMS.Data;
using RSMS.Repositories.Contracts;
using RSMS.Repositories.Implementation;
using RSMS.Services.Contracts;
using RSMS.Services.Implementation;
using RSMS.Services.Implementations;
using RSMS.Services.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Bind JWT configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

// Add DbContext
builder.Services.AddDbContext<RSMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RsmsDb")));

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<ISchoolService, SchoolService>();

// Lookup Repository
builder.Services.AddScoped(typeof(ILookupRepository<,>), typeof(LookupRepository<,>));

// Lookup Service
builder.Services.AddScoped(typeof(ILookupService<,>), typeof(LookupService<,>));

// Register AutoMapper
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(config => { /* Optional config here */ },
                              AppDomain.CurrentDomain.GetAssemblies());



// Swagger
builder.Services.AddEndpointsApiExplorer();
// Swagger Config with Authorization
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "RSMS API", Version = "v1" });

    // Add JWT auth to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter: Bearer {your_jwt_token}"
    });

    // RSHostelId header
    c.AddSecurityDefinition("RSHostelId", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "RSHostelId",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Enter the RSHostelId (required for non-SuperAdmin users)"
    });

    // Apply both security definitions globally
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        },
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "RSHostelId"
                }
            },
            new string[] {}
        }
    });
});

// JWT Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Authorization
builder.Services.AddAuthorization();

// Optional: API clients
builder.Services.Configure<List<ApiClient>>(builder.Configuration.GetSection("Clients"));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
// Configure controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
var app = builder.Build();

app.UseRouting();
// Order is important:
app.UseCors("AllowFrontend");   // 👈 Apply CORS before Auth

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add middleware
//app.UseHttpsRedirection(); //Commeting for Deployment purpose need to be undo for proper deployment
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
