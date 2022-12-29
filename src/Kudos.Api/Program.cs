using Kudos.Api;
using Kudos.Api.Middlewares;
using Kudos.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Diagnostics.Metrics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
   o.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "KUDO API", 
        Version = "v1",
        Description = "An API to Kudos operations",
        Contact = new OpenApiContact
        {
            Name = "Mindaugas Baltrunas",
            Email = "mindebaltru@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/mindaugas-baltrunas-55462a155/"),
        },
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService <AppDbContext> ();
    context.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
