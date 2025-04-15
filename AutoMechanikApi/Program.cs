using AutoMechanikCore.Data;
using AutoMechanikCore.Models.Enums;
using AutoMechanikCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AutoMechanikDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AutoMechanikDbContextConnection' not found.");
builder.Services.AddDbContext<AutoMechanikDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDataProtection();

builder.Services.AddIdentityCore<AutoMechanikUser>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddRoles<AutoMechanikRole>()
	.AddEntityFrameworkStores<AutoMechanikDbContext>()
	.AddDefaultTokenProviders();

// Settings register password
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 1;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	await SeedData.Initialize(services);
}

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
