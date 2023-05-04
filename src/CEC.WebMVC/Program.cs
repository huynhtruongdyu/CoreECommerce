using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // Cache static files for 30 days
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=60");
            ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddMinutes(1).ToString("R", CultureInfo.InvariantCulture));
        }
    });

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}