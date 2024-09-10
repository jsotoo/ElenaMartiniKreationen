using ElenaMartiniKreationen.DataAccess;
using ElenaMartiniKreationen.DataAccess.Data;
using ElenaMartiniKreationen.Server.Configuration;
using ElenaMartiniKreationen.Server.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRepositories().AddServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuramos el contexto de seguridad del API
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                           throw new InvalidOperationException("No se configuro el SecretKey"));

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Emisor"],
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});


builder.Services.Configure<AppSettings>(builder.Configuration);


builder.Services.AddDbContext<ElenaMartiniKreationenDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElenaMartiniKreationenDb"));
    options.EnableSensitiveDataLogging();

});


// Configuramos ASP.NET Identity
builder.Services.AddIdentity<IdentityUserElenaMartiniKreationen, IdentityRole>(policies =>
{
    policies.Password.RequireDigit = false;
    policies.Password.RequireLowercase = false;
    policies.Password.RequireUppercase = true;
    policies.Password.RequireNonAlphanumeric = true;
    policies.Password.RequiredLength = 8;

    policies.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<ElenaMartiniKreationenDbContext>()
  .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAllOrigins");
app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    await UserDataSeeder.Seed(scope.ServiceProvider);
}
app.Run();
