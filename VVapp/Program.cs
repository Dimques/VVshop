using VVapp.DbProviders;
using VVapp.JsonConverters;
using VVapp.Loggers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new OuterwearMetaConverter());
});
services.AddHttpContextAccessor();
services.AddSingleton<ILog, ConsoleLog>();
services.AddSingleton<IDbProvider, TxtJpgDataBase>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/index.html");
    });
});

app.Run();