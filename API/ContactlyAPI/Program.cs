using ContactlyAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(); // allows the import of controller for API to work
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// injecting the DbContext class - means we can use it inside controller and different classes 
builder.Services.AddDbContext<ContactlyDbContext>(options => options.UseInMemoryDatabase("ContactsDb")); // passes parameters of options s.t it creates a database named ContactsDb

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); // defines a policy that accepts anything from anywhere - any port possible

app.MapControllers(); // Mapping the controllers correctly to API scheme

app.Run();
