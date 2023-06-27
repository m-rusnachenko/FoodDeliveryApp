global using FoodDeliveryApi.Data;

global using FoodDeliveryApi.Services.AuthService;
global using FoodDeliveryApi.Services.OrderService;
global using FoodDeliveryApi.Services.ProductsService;
global using FoodDeliveryApi.Services.ShopService;
global using FoodDeliveryApi.Services.UserService;

global using Microsoft.EntityFrameworkCore;
global using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FoodDeliveryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql_Somee"));
    // options.UseSqlite("Data Source=food-delivery-app.db");
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "User.Cookie";
        config.LoginPath = "/api/auth/login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        config.SlidingExpiration = true;
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
    options.AddPolicy("Manager", policy => policy.RequireClaim("Manager"));
    options.AddPolicy("User", policy => policy.RequireClaim("User"));
});

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddConsole()
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
    loggingBuilder.AddDebug();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", (HttpContext ctx) => ctx.Response.Redirect("/swagger"));

app.Run();