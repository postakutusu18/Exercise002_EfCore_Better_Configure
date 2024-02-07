namespace Exercise002_EfCore_Better_Configure.Response;

public class CompanyResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CompanyResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
