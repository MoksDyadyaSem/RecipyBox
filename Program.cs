using Microsoft.EntityFrameworkCore;
using RecipeBox.Api;
using RecipeBox.Dto;
using RecipeBox.Repositories;
using RecipeBox.Services;
using RecipeBox.database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<RecipeBoxDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IExpenseTransactionRepository, ExpenseTransactionRepository>();

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RecipeBoxDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

var api = app.MapGroup("/api");
api.MapCategoriesEndpoints();
api.MapRecipesEndpoints();
api.MapCustomersEndpoints();
api.MapOrdersEndpoints();
api.MapExpensesEndpoints();

app.MapGet("/", () => "открой /swagger");

app.Run();
