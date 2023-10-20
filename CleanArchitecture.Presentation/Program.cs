using CleanArchitecture.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterApplicationServices()
                .RegisterInfrastructureServices()
                .RegisterPersistenceServices(builder.Configuration)
                .RegisterPresentationServices();


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




app.Run();
