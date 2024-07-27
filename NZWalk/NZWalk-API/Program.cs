using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using NZWalk_API.Data;
using NZWalk_API.Mappings;
using NZWalk_API.Repositories;
using NZWalk_API.Repositories.Auth;
using NZWalk_API.Repositories.Interface;
using NZWalk_API.Repositories.Walk_Repository;
using NZWalk_API.Repositories.Walk_Repository.Interface;
using System.Text;
using NZWalk_API.Repositories.Image_Repository;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region lOGGER configuration

var logger = new LoggerConfiguration()
.WriteTo.Console()
.WriteTo.File("Logs/NzWalks_Log.txt",rollingInterval:RollingInterval.Minute)
.MinimumLevel.Information()
.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Options=>
{
    Options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NZ Walk API",
        Version = "v1"

    });
    Options.AddSecurityDefinition(
        JwtBearerDefaults.AuthenticationScheme,new OpenApiSecurityScheme
        {
            Name="Authorization",
            In=ParameterLocation.Header,
            Type=SecuritySchemeType.ApiKey,
            Scheme=JwtBearerDefaults.AuthenticationScheme,
        });
    Options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme,
                },Scheme="Oauth2",
                Name=JwtBearerDefaults.AuthenticationScheme,
                In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
}

);
builder.Services.AddDbContext<NZWalksDBContext>(options => options.
UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

//Auth In
builder.Services.AddDbContext<NZWalkAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkAuthConnectionString")));

builder.Services.AddScoped<IRegionRepository,RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<ITokenRepository,TokenRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


//Identity Solution 
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()//AddingRole
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalkAuthDbContext>()
    .AddDefaultTokenProviders();

//configur Identity 
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit=false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;


    



}
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer=true
        ,ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        
    });


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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ImageStore")),
    RequestPath= "/ImageStore"
});

app.MapControllers();

app.Run();
