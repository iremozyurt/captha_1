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
 Razor Pages, bu yap�y� daha basit bir �ekilde sunar. 
Her sayfa, hem veri i�lemleri hem de kullan�c�ya g�sterilecek HTML 
i�eri�i i�in gerekli mant��� i�erir. Bu, �zellikle CRUD (Create, Read, Update, Delete) 
operasyonlar�n� ger�ekle�tiren sayfalar olu�tururken kodun daha okunabilir ve
y�netilebilir olmas�n� sa�lar.
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
 Bu sat�r, gelen HTTP isteklerini belirli bir 
denetleyici (controller) ve eyleme (action) y�nlendirmek i�in kullan�l�r.
 */
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
