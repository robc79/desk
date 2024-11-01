using Desk.Domain.Entities;
using Desk.Domain.Repositories;
using Desk.Infrastructure.Sql;
using Desk.Infrastructure.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Desk.Application.UseCases.ViewTag;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Desk.Application.Configuration;
using Desk.Application.Identity.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(path: "Logs\\log.json", rollingInterval: RollingInterval.Day, formatter: new JsonFormatter())
    .CreateLogger();

Log.Information("Atomic batteries to power, turbines to speed.");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddRazorPages(options => {
        options.Conventions.AuthorizeFolder("/Items");
        options.Conventions.AuthorizeFolder("/Tags");
    });
    
    builder.Services
        .AddDefaultIdentity<User>(options =>
        {
            options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:RequireAuthenticatedEmail");
        })
        .AddEntityFrameworkStores<DeskDbContext>();
    
    if (builder.Configuration.GetValue<bool>("Identity:RequireAuthenticatedEmail"))
    {
        builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
    }
    else
    {
        builder.Services.AddTransient<IEmailSender, DummyEmailSender>();
    }

    builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<ViewUserTagHandler>());

    builder.Services.AddDbContext<DeskDbContext>(options =>
    {
        var connString = builder.Configuration.GetConnectionString("Desk");
        options.UseSqlServer(connString);
    });

    builder.Services.AddScoped<IUnitOfWork>(services => services.GetRequiredService<DeskDbContext>());
    builder.Services.AddScoped<ITagRepository, TagRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IItemRepository, ItemRepository>();
    builder.Services.AddScoped<ITextCommentRepository, TextCommentRepository>();

    var emailSenderConfig = new EmailSenderConfiguration();
    builder.Configuration.GetSection(EmailSenderConfiguration.SectionName).Bind(emailSenderConfig);
    builder.Services.AddSingleton(emailSenderConfig);

    var identityConfig = new IdentityConfiguration();
    builder.Configuration.GetSection(IdentityConfiguration.SectionName).Bind(identityConfig);
    builder.Services.AddSingleton(identityConfig);

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start.");
}
finally
{
    Log.Information("Application shutdown complete.");
    Log.CloseAndFlush();
}