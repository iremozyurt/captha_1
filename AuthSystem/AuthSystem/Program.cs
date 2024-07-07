using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

/*
 Razor Pages, bu yapýyý daha basit bir þekilde sunar. 
Her sayfa, hem veri iþlemleri hem de kullanýcýya gösterilecek HTML 
içeriði için gerekli mantýðý içerir. Bu, özellikle CRUD (Create, Read, Update, Delete) 
operasyonlarýný gerçekleþtiren sayfalar oluþtururken kodun daha okunabilir ve
yönetilebilir olmasýný saðlar.
 */


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();


/*
 Bu satýr, gelen HTTP isteklerini belirli bir 
denetleyici (controller) ve eyleme (action) yönlendirmek için kullanýlýr.
 */
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
