using Npgsql;

namespace Application.Data;

public class Database
{
    private string connectionString = 
    "Host = localhost; Port = 5432; Database = crm; Username = postgres; Password = 1234";
    public NpgsqlConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}
