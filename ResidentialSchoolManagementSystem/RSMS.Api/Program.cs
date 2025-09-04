using Microsoft.OpenApi.Models;
using RSMS.Data;
using RSMS.Services.Interfaces;
using RSMS.Services.Implementations;

using Microsoft.EntityFrameworkCore;
using RSMS.Business.Contracts;
using RSMS.Business.Implementation;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<RsmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RsmsDb")));

// Register Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IAssetManager, AssetManager>();  
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IAttendanceManager, AttendanceManager>();
builder.Services.AddScoped<IInventoryManager, InventoryManager>();
builder.Services.AddScoped<IRoleManager, RoleManager>();
builder.Services.AddScoped<IStaffManager, StaffManager>();
builder.Services.AddScoped<IStudentManager, StudentManager>();
builder.Services.AddScoped<IUserManager, UserManager>();

// Add Controllers
builder.Services.AddControllers();

// Swagger Config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RSMS API",
        Version = "v1",
        Description = "Residential School Management System API",
        Contact = new OpenApiContact
        {
            Name = "Praneeth Pagunta",
            Email = "praneethpagunta@tconnectservices.com"
        }
    });
});

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RSMS API v1");
        c.RoutePrefix = string.Empty; // Swagger at root (http://localhost:5000)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
