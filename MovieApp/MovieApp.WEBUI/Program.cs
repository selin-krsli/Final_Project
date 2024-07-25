using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieApp.BUSINESS.Abstract;
using MovieApp.BUSINESS.Concrete;
using MovieApp.DATA.Abstract;
using MovieApp.DATA.Concrete.EfCore;
using MovieApp.WEBUI.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=MovieDb"));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;

    //Logout
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    //Cookie ve Session birbirini tanýmýyorsa;
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true; //Cookie süresinin sýfýrlanmasý.
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".MovieApp.Security.Cookie",
    };
});

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
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "adminuseredit",
//    pattern: "admin/user/{id?}",
//    defaults: new { controller = "Admin", action = "EditUser" }
//    );

//app.MapControllerRoute(
//    name: "adminusers",
//    pattern: "admin/user/list",
//    defaults: new { controller = "Admin", action = "UserList" }
//    );

//app.MapControllerRoute(
//    name: "adminroles",
//    pattern:"admin/role/list",
//    defaults: new {controller="Admin", action="RoleList"}
//    );
//app.MapControllerRoute(
//    name: "adminrolecreate",
//    pattern: "admin/role/create",
//    defaults: new { controller = "Admin", action = "CreateRole" }
//    );
//app.MapControllerRoute(
//    name: "adminroleedit",
//    pattern: "admin/role/{id?}",
//    defaults: new { controller = "Admin", action = "EditRole" }
//    );
//app.MapControllerRoute(
//    name: "adminmovies",
//    pattern: "admin/movies",
//    defaults: new {controller = "Admin", action="MovieList"}
//    );
//app.MapControllerRoute(
//    name: "adminmoviecreate",
//    pattern: "admin/movies/create",
//    defaults: new { controller = "Admin", action = "CreateMovie" }
//    );
//app.MapControllerRoute(
//    name: "adminmovieedit",
//    pattern: "admin/movies/{id?}",
//    defaults: new { controller = "Admin", action = "EditMovie" }
//    );
//app.MapControllerRoute(
//    name: "admincategories",
//    pattern: "admin/categories",
//    defaults: new { controller = "Admin", action = "CategoryList" }
//    );
//app.MapControllerRoute(
//    name: "admincategorycreate",
//    pattern: "admin/categories/create",
//    defaults: new { controller = "Admin", action = "CreateCategory" }
//    );
//app.MapControllerRoute(
//    name: "admincategoryedit",
//    pattern: "admin/categories/{id?}",
//    defaults: new { controller = "Admin", action = "EditCategory" }
//    );

//app.MapControllerRoute
//    (
//      name: "search",
//      pattern: "search",
//      defaults: new { controller="Movie", action="Search"}
//    );

//app.MapControllerRoute(
//    name: "moviedetails",
//    pattern: "{url}",
//    defaults: new {controller="Movie", action="Details"}
//    );
//app.MapControllerRoute(
//    name: "movies",
//    pattern: "movies/{category?}",
//    defaults: new {controller="Movie", action="List"}
//    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapDefaultControllerRoute();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var configuration = services.GetRequiredService<IConfiguration>();

    await SeedIdentity.Seed(userManager, roleManager, configuration);
}
app.Run();
