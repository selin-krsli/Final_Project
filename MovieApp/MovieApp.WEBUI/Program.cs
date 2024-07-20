using MovieApp.DATA.Abstract;
using MovieApp.DATA.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieRepository, EfCoreMovieRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapDefaultControllerRoute();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
