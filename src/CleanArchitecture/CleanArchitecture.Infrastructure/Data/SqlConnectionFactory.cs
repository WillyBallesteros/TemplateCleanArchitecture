using System.Data;
using CleanArchitecture.Application.Abstractions.Data;
using Npgsql;

namespace CleanArchitecture.Infrastructure.Data;


internal sealed class sqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public sqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}
