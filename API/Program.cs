using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext> (opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); // Our db needs a connection string, with the help of SqLite, go to configuration: `appsettings.Development.json`
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// Added manually in the course
using var scope = app.Services.CreateScope(); // once scope is done, everything inside `scope` is going to be disposed or destroyed and cleaned up from memory
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync(); // add `await`, change Migrate() to MigrateAsync() because the next step is async, this method must match.
    await Seed.SeedData(context); // add `await` because Seed uses async task. `await` means system waits for a task-completion notification to move on.
}
catch (System.Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}

// End of the added codes

app.Run();
