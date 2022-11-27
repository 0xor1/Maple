using Dnsk.Db;
using Dnsk.Service.Services;
using Dnsk.Service.Util;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc(opts =>
{
    opts.Interceptors.Add<ErrorInterceptor>();
});
builder.Services.AddScoped<ISessionManager, SessionManager>();
builder.Services.AddDbContext<DnskDb>(
    dbContextOptions =>
    {
        var cnnStrBldr = new MySqlConnectionStringBuilder(Config.Db.Connection);
        cnnStrBldr.Pooling = true;
        cnnStrBldr.MaximumPoolSize = 100;
        cnnStrBldr.MinimumPoolSize = 1;
        var version = new MariaDbServerVersion(new Version(10, 8));
        dbContextOptions
            .UseMySql(cnnStrBldr.ToString(), version, opts => { opts.CommandTimeout(1); });
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.MapGrpcService<ApiService>();
app.MapFallbackToFile("index.html");
app.Run(Config.Server.Listen);
