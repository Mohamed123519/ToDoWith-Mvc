var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Cookie").AddCookie("Cookie");

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly", p =>
    {
        p.RequireClaim("Role", "Admin");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.Use(async (context, next) =>
//{
//    var oldCookie = context.Request.Cookies["Cookie"];
//    if (oldCookie != null)
//    {
//        context.Response.Cookies.Delete("Cookie");
//        context.Response.Cookies.Append("Cookie", oldCookie, new CookieOptions()
//        {
//            Expires = DateTime.UtcNow.AddMonths(12)
//        });
//    }
//    await next.Invoke();
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();
