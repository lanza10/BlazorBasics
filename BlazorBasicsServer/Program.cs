using BlazorBasicsServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

//Types of loggers (by default is Information):

/* 
 * Trace: Specific and detailed info, useful for intensive debug.
   Debug: Debug info that may be useful during development.
   Information: info messages about the correct working of the app.
   Warning: warnings about situations that could evolve into errors in the future
   Error: no critic errors that does not prevent the normal working of the app
 * Critical: critical errors that can 
 */


//Levels of configuration files

/*
 * app.settings.json
   app.settings.<environment>.json
   App secrets (secrets.json) - Only development mode -> ruta : %APPDATA%\Microsoft\UserSecrets (or right click on project -> Manage user secrets)
   Environment Variables
   CMD
 * Azure Key Vaults
 */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<WeatherForecastService>();

// Add dependency injection:
//  -(singleton only gives one for execution)
//  -(scoped gives one new for each session)
//  -(transient gives one new each time is called)
builder.Services.AddTransient<IDataDemo, DataDemo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
