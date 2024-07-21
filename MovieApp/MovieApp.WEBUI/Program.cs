using MovieApp.BUSINESS.Abstract;
using MovieApp.BUSINESS.Concrete;
using MovieApp.DATA.Abstract;
using MovieApp.DATA.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieRepository, EfCoreMovieRepository>();
builder.Services.AddScoped<IMovieService, MovieManager>();

builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
var app = builder.Build();

app.UseHttpsRedirection();

if(app.Environment.IsDevelopment())
{
    SeedData.Seed();
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "moviedetails",
//    pattern: "{url}",
//    defaults: new {controller="Movie", action="Details"}
//    );
app.MapControllerRoute(
    name: "movies",
    pattern: "movies/{category?}",
    defaults: new {controller="Movie", action="List"}
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapDefaultControllerRoute();
app.Run();
