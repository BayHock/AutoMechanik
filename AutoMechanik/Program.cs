using AutoMechanik.Areas.Identity.Data;
using AutoMechanik.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AutoMechanikDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AutoMechanikDbContextConnection' not found.");

builder.Services.AddDbContext<AutoMechanikDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddIdentity<AutoMechanikUser, AutoMechanikRole>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<AutoMechanikDbContext>()
	.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	await SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
