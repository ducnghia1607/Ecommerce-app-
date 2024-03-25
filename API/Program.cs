using API;
using API.Errors;
using API.Extensions;
using Core;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
    RequestPath = "/Content"
});
app.MapFallbackToController("Index", "Fallback");

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//     Get service of type serviceType from the System.IServiceProvider.
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    // Asynchronously applies any pending migrations for the context to the database. Will create the database if it does not already exist.
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
