using Microsoft.EntityFrameworkCore;
using PosterCMS;
using PosterCMS.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PosterDbContext>(options =>
    options.UseInMemoryDatabase("PosterDB")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

ImageManager.readyUp();

createDir("uploads");
createDir("thumbnails");

app.Run();

void createDir(string folder)
{
    var webRootPath = app.Environment.WebRootPath;
    var uploadsPath = Path.Combine(webRootPath, folder);

    if (!Directory.Exists(uploadsPath))
    {
        Directory.CreateDirectory(uploadsPath);
    }
}
