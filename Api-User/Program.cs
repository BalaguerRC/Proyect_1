using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api_User.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Api_User.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Api_UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Api_UserContext") ?? throw new InvalidOperationException("Connection string 'Api_UserContext' not found.")));

//string issuer = builder.Configuration["Jwt:Issuer"];

//token

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// Add services to the container.

//add context accesor
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriServics(uri);
});

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



app.MapControllers();

app.Run();
