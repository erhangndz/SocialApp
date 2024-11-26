using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Context;
using ServerApp.Models;
using ServerApp.Services;
using ServerApp.Settings;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SocialContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<SocialContext>();

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



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetRequiredSection("JwtOptions").Get<JwtOptions>();
    
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidIssuer=jwtOptions.Issuer,
        ValidAudience=jwtOptions.Audience,
        ValidateIssuer = true,
        ValidateAudience= true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Key)),
        ValidateLifetime = true,
        ClockSkew= TimeSpan.Zero

    };
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped<ITokenService,TokenService>();
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

