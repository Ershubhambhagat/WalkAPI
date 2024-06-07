using Microsoft.EntityFrameworkCore;
using NZWalk_API.Data;
using NZWalk_API.Mappings;
using NZWalk_API.Repositories;
using NZWalk_API.Repositories.Interface;
using NZWalk_API.Repositories.Walk_Repository;
using NZWalk_API.Repositories.Walk_Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDBContext>(options => options.
UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
builder.Services.AddScoped<IRegionRepository,RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));




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
