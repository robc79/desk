using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Desk.Infrastructure.Sql;
using Desk.Infrastructure.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Desk.Application.UseCases.ViewTag;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<User>().AddEntityFrameworkStores<DeskDbContext>();

builder.Services.AddDbContext<DeskDbContext>(options => {
    var connString = builder.Configuration.GetConnectionString("Desk");
    options.UseSqlServer(connString);
});

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<ViewUserTagHandler>());

builder.Services.AddScoped<IUnitOfWork>(services => services.GetRequiredService<DeskDbContext>());
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
