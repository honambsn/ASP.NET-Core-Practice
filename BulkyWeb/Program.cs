using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//update this for hot reload
//builder.Services.AddControllersWithViews();


if (builder.Environment.IsDevelopment())
{
    // For development, use the default controller with views
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
}
else
{
    // For production, use a minimal API approach
    builder.Services.AddControllersWithViews();
}

builder.Services.AddDbContext<BulkyWeb.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
