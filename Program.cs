using Microsoft.EntityFrameworkCore;
using InventoryApi.DBContext;
using InventoryApi.Repositories;
using InventoryApi.Repositories.Interfaces;
using InventoryApi.Services.Interfaces;
using InventoryApi.Services;
using InventoryApi.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Configuración de DbContexts
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registro de repositorios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

// Registro de servicios
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowLocalhost5173",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseCors("AllowLocalhost5173");
app.UseAuthorization();
app.MapControllers();

app.Run();
