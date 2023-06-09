using CEC.Infrastructure.Extensions;
using CEC.Shared.Options;

using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
    });
    builder.Services.AddControllersWithViews();
    builder.Services.Configure<GlobalAppsettings>(x => builder.Configuration.Bind(x));
    builder.Services.RegisterService();
    builder.Services.AddAndMigrateTenantDatabases();
    builder.Services.AddResponseCaching();
}

var app = builder.Build();
{
    app.UseResponseCompression();
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // Cache static files for 1 minute
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=60");
            ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddMinutes(1).ToString("R", CultureInfo.InvariantCulture));
        }
    });

    app.UseRouting();

    app.UseAuthorization();

    #region Map endpoints

    app.MapAreaControllerRoute(
        name: "defaultArea",
        areaName: "Marketing",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    #endregion Map endpoints

    app.UseResponseCaching();

    app.Run();
}