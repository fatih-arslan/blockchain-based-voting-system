using DataAccess.Data;
using DataAccess.Repositories;
using DataAccess.Repositories.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Contracts;
using Services.Utilities;
using Services.Utilities.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IElectionRepository, ElectionRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IElectionService, ElectionService>();   
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var defaultImagePaths = builder.Configuration.GetSection("DefaultImages").Get<DefaultImagePaths>() ??
    throw new InvalidOperationException("Default images configuration not found.");
builder.Services.AddSingleton(defaultImagePaths);

builder.Services.AddIdentity<ApplicationUser , IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddRoles<IdentityRole>();

builder.Services.AddSession();
builder.Services.AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.ApplyDatabaseMigrations(app);
AppDbInitializer.SeedUsersAndRoles(app).Wait();
app.Run();
