using ABtesting.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDevicesService, DevicesService>();
builder.Services.AddScoped<IExperimentService, ExperimentService>();
builder.Services.AddScoped<IDevicesExperimentService, DevicesExperimentService>();
builder.Services.AddTransient<IRandomProvider, RandomProvider>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddDbContextPool<ApplicationContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Applications")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();