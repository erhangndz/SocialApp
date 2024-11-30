using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Context;
using ServerApp.Data;
using ServerApp.Models;
using ServerApp.Repositories;
using ServerApp.Services;
using ServerApp.Services.UserServices;
using ServerApp.Settings;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();
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
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var userManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<AppUser>>();

   
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler(error =>
    {
        error.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var exception = context.Features.Get<IExceptionHandlerFeature>();
            if (exception != null)
            {
                //loglama => 
                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Error.Message
                }.ToString());
            }
        });
    });
    SeedDatabase.Seed(userManager).Wait();
}

app.UseCors("SocialApp");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


app.Run();

