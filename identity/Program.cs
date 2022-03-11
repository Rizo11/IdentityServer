using System;
using System.Security.AccessControl;
using identity.Entity;
using System.Reflection;
using identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IdentityServer4.EntityFramework;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddIdentityServer()
.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = b 
        => b.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"),
        sql  => sql.MigrationsAssembly(migrationsAssembly));
    
})
.AddOperationalStore(options =>
{
    options.ConfigureDbContext = b 
        => b.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"),sql 
            => sql.MigrationsAssembly(migrationsAssembly));
})
.AddDeveloperSigningCredential();

var app = builder.Build();


app.MapDefaultControllerRoute();

app.Run();
