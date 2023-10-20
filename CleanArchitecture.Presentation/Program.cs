using CleanArchitecture.Presentation;
using CleanArchitecture.Presentation.HealthChecks;
using Microsoft.EntityFrameworkCore;

using HealthChecks.UI.Core;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterApplicationServices()
                .RegisterInfrastructureServices()
                .RegisterPersistenceServices(builder.Configuration)
                .RegisterPresentationServices();

builder.Services.AddHealthChecks();

builder.Services.AddHealthChecks()
         .AddSqlServer(
    builder.Configuration.GetConnectionString("ApplicationDbContext")
    , tags: new[] { "database" }
    )
         .AddCheck<MyHealthCheck>("MyHealthCheck", tags: new[] { "custom" });
         

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scop = app.Services.CreateScope())
{
    var serviceProvider = scop.ServiceProvider;

    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    if (dbContext is not null)
        await dbContext.Database.MigrateAsync();


}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture v1"));

app.UseCors("CorsPolicy");

app.UseHealthChecks("/healthcheck", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse //nuget: AspNetCore.HealthChecks.UI.Client
});

//nuget: AspNetCore.HealthChecks.UI
app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui";
    options.ApiPath = "/health-ui-api";
});

app.Run();
