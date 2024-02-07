namespace Exercise002_EfCore_Better_Configure.Options;
public class DatabaseOptions
{
    public string ConnectionStrings { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; }
    public int CommandTimeOut { get; set; }
    public bool EnableDetailedErrors { get; set; }
    public bool EnableSensitiveDataLogging { get; set; }
}
