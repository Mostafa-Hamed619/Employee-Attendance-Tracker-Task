using EmployeeAttendance.Business.Implementations;
using EmployeeAttendance.Business.Interfaces;
using EmployeeAttendance.DataContext.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// applying the db services using InMemory datbaase
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseInMemoryDatabase("AttendanceDb"));

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.EnsureCreated();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Department}/{action=Index}")
    .WithStaticAssets();


app.Run();
