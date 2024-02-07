using Exercise002_EfCore_Better_Configure;
using Exercise002_EfCore_Better_Configure.Models;
using Exercise002_EfCore_Better_Configure.Options;
using Exercise002_EfCore_Better_Configure.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
//builder.Services.AddDbContext<DatabaseContext>(dbContextOptionBuilder =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("Database");
//    dbContextOptionBuilder.UseSqlServer(connectionString,sqlServerAction =>
//    {
//        sqlServerAction.EnableRetryOnFailure(3);
//        sqlServerAction.CommandTimeout(30);
//    });
//    dbContextOptionBuilder.EnableDetailedErrors(true);
//    dbContextOptionBuilder.EnableSensitiveDataLogging(true);
//});

builder.Services.AddDbContext<DatabaseContext>((serviceProvider, dbContextOptionBuilder) =>
{
    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
    dbContextOptionBuilder.UseSqlServer(databaseOptions.ConnectionStrings, sqlServerAction =>
    {
        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeOut);
    });
    dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
    dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
});


var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("companies/{companyId:int}",async (int companyId,DatabaseContext dbContext) =>
{
    var company =
    await dbContext.Set<Company>()
    .AsNoTracking()
    .FirstOrDefaultAsync(c => c.Id==companyId);

    if (company is null)
    {
        return Results.NotFound($"The Company with Id '{companyId}' was not found");
    }
    var response = new CompanyResponse(company.Id, company.Name);
    return Results.Ok(response);
});

app.Run();

