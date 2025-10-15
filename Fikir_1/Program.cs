using Fikir_1.Services; // Servisi projenin içine dahil et

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Session desteðini ekledik
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); //30 dakika boyunca oturum açýk kalacak
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//OpenAI Servisini Dependency Injection'a ekle
builder.Services.AddSingleton<OpenAIService>();

//OpenAI API Key'i appsettings.json' dan okumak için yapýlandýrma
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddHttpClient(); // deniyorum
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// ?? Uygulama kapatýlýrken `uploads` Klasörünü Temizle
var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

lifetime.ApplicationStopping.Register(() =>
{
    string uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");


    if (Directory.Exists(uploadFolderPath))
    {
        var files = Directory.GetFiles(uploadFolderPath);
        foreach (var file in files)
        {
            File.Delete(file);
        }

    }

});



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Legend}/{action=Index}/{id?}");

app.Run();
