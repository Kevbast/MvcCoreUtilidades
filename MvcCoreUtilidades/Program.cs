using MvcCoreUtilidades.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//vamos a implementarlo puesto que vamos a utilizarlo en varios sitios
builder.Services.AddSingleton<HelperPathProvider>();
//EL TRANSIENT PARA USAR DATOS
//EL SINGLETON ES ESTÁTICO,NO VA A CAMBIAR

builder.Services.AddHttpContextAccessor();

//vamos a añadirmemorycache para el nuevo controllador
builder.Services.AddDistributedMemoryCache();
//añadimos otro para el cache personalizado antes haber instalado su nugget
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//IMPORTANTE IMPLEMENTARLO
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
