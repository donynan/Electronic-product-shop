using Microsoft.EntityFrameworkCore;
using MyShop.Infrastructure;
using MyShop.Application;
using MyShop.Infrastructure.DbContexts;
using MyShop.Infrastructure.Common;
using MyShop.Web.Middleware;
using Microsoft.AspNetCore.Identity;
using MyShop.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationService();
builder.Services.AddInfraStructureService();
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());
});
#region Database Connectivity

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{

}).AddEntityFrameworkStores<ApplicationDbContext>();
#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title ="MaxiShop API version 1",
        Description = "Developed by codewithkarthik",
        Version = "v1.0"
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "MaxiShop API version 2",
        Description = "Developed by codewithkarthik",
        Version = "v2.0"
    });


    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description = @"Jwt Authorization header using the Bearer Scheme.
                        Enter 'Bearer' [space] and then your token in the input below.
                        Example:'Bearer 12345abcdef'",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                },
                Scheme ="Oauth2",
                Name="Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

#region Configuration for Seeding Data to Database

static async void UpdateDatabaseAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            await SeedData.SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogError(ex, "An error occurred while migrating or seeding the database");
        }
    }
}

#endregion

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
UpdateDatabaseAsync(app);
var ServiceProvider = app.Services;
await SeedData.SeedRoles(ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
