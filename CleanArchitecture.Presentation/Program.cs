using CleanArchitecture.Presentation;
using Microsoft.EntityFrameworkCore;

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


app.Run();
