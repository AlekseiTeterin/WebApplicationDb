using Microsoft.EntityFrameworkCore;
using WebApplicationDB;
using WebApplicationDB.Models;
using WebApplicationDB.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "myapp.db";
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
   options => options.UseSqlite($"Data Source={dbPath}"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// "/add_product" - RPC
// "/catalog/products" - REST
app.MapPost("/add_product", async ([FromBody] Product product, [FromServices] AppDbContext dbContext) =>
{
    await dbContext.Products.AddAsync(product);
    await dbContext.SaveChangesAsync();
}).WithName("Добавляет товар");

app.MapGet("/get_products", ([FromServices] AppDbContext dbContext) =>
{
    return dbContext.Products.ToListAsync();
}).WithName("Получает товар");

app.MapGet("/get_product", async([FromServices] AppDbContext dbContext,
    [FromQuery] long productId) => await dbContext.Products
    .FirstOrDefaultAsync(p => p.Id == productId));

app.MapPost("/update_product",
    async ([FromServices] AppDbContext dbContext,
        [FromQuery] int productId, string name, decimal price) =>
    {
        var product = await dbContext.Products.FindAsync(productId);
        product!.Name = name;
        product.Price = price;
        dbContext.Products.Update(product!);
        await dbContext.SaveChangesAsync();
    });

app.MapPost("/delete_product",
    async ([FromServices] AppDbContext dbContext,
    [FromQuery] int productId) =>
    {
        var product = await dbContext.Products.FindAsync(productId);
        dbContext.Products.Remove(product!);
        await dbContext.SaveChangesAsync();
    });

app.Run();

