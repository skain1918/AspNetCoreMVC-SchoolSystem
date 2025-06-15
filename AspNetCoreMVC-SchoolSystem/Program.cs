using AspNetCoreMVC_SchoolSystem;
using AspNetCoreMVC_SchoolSystem.Models;
using AspNetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Add service to the database
builder.Services.AddDbContext<SchoolDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection"));
});
//builder.Services.AddDbContext<SchoolDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDbConnection"));
//});
//builder.Services.AddDbContext<SchoolDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MonstrerAspConnection"));
//});
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<SchoolDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<GradeService>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

});
// Configure the application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);      // Set the expiration time for login cookies
    options.SlidingExpiration = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseDeveloperExceptionPage();
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

app.Run();
