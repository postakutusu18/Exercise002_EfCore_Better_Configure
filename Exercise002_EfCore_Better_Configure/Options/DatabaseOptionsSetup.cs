using Microsoft.Extensions.Options;

namespace Exercise002_EfCore_Better_Configure.Options;
public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{

    private const string ConfigurationSectionName = "DatabaseOptions";
    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString("Database");
        options.ConnectionStrings = connectionString;

        _configuration.GetSection(ConfigurationSectionName).Bind(options);


    }
}
