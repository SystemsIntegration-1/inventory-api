using inventory_api.src.Validators;
using InventoryApi.Aplication.Services;
using InventoryApi.Aplication.Services.Interfaces;
using InventoryApi.Config;
using InventoryApi.Infrastructure.DBContext;
using InventoryApi.Infrastructure.Repositories;
using InventoryApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();

builder.Services.AddValidations();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IBatchService, BatchService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowLocalhost5173",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost5173");
app.UseAuthorization();
app.MapControllers();
app.Run();
