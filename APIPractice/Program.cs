using Microsoft.EntityFrameworkCore;
using Practice.Services;
using Practice.Data;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUserStorageEFRepo, UserStorageEFRepo>();
builder.Services.AddScoped<IItemStorageEFRepo, ItemStorageEFRepo>();

string connectionString = File.ReadAllText(@"C:\Tools\connection.txt");

builder.Services.AddDbContext<PracticeContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
