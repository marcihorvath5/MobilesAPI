using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobilesApi.Migrations;
using MobilesApi.Models;
using MobilesApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMobileService, MobileService>();
builder.Services.AddDbContext<MobileDb>(db =>
{
    db.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MobileDb>()
                .AddDefaultTokenProviders();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await CreateRoles(roleManager);
}

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
{
    string[] roles = ["Admin", "User", "Worker"];

    foreach (var role in roles)
    {
        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            var newRole = new IdentityRole(role);
            await roleManager.CreateAsync(newRole);
        }
    }
}