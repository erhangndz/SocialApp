using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ServerApp.Context;
using ServerApp.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SocialContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<SocialContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("SocialApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("SocialApp");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


app.Run();

