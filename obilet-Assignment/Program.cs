using obilet_Assignment.Base;
using obilet_Assignment.Interface;
using obilet_Assignment.Mapper;
using ObiletBusiness;
using ObiletBusiness.Cache;
using ObiletBusiness.Interfaces;
using ObiletBusiness.Request;
using ObiletBusiness.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<Options>(
   builder.Configuration.GetSection("EnviromentOptions"));
builder.Services.AddTransient<ITravelService, TravelService>();
builder.Services.AddTransient<IBrowserInteractionService, BrowserInteractionService>();
builder.Services.AddTransient<IObiletBase, ObiletBase>();
builder.Services.AddTransient<ICustomHttpClient, CustomHttpClient>();
builder.Services.AddSingleton<IRedisCacheManager, RedisCacheManager>();

//var redisConfig = builder.Configuration.GetValue<string>("RedisConfig");
//builder.Services.AddSingleton<IRedisCacheManager>(new RedisCacheManager(redisConfig));

builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
