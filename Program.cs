using CSM_Registro.Data;
using CSM_Registro.Data.Abstraccion;
using CSM_Registro.Data.Repositories;
using CSM_Registro.Services.Implementacion;
using CSM_Registro.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.Configure<MongoDbSettings>
    (
    builder.Configuration.GetSection("MongoDbSettings")
    );

builder.Services.AddSingleton<MongoDbRepositorio>();
builder.Services.AddScoped<IMongoDb, MongoDbRepositorio>();
builder.Services.AddScoped<IAsociadoService, AsociadoService>();












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
    pattern: "{controller=Asociado}/{action=Registro}/{id?}");

app.Run();
