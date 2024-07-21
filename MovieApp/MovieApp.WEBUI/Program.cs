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

app.UseStaticFiles();
if(app.Environment.IsDevelopment())
{
    SeedData.Seed();
    app.UseDeveloperExceptionPage();
}

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
