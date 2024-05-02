using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories;

public class Repository : IRepository
{
    private readonly IConfiguration _configuration;
    
    public Repository(
        IConfiguration configuration
        )
    {
        _configuration = configuration;
    }

    public async Task<List<TRes>> QueryListAsync<TRes, T>(string command, T parameters)
    {
        using IDbConnection connection = new MySqlConnection(_configuration.GetConnectionString("MySql"));
        return (await connection.QueryAsync<TRes>(command, parameters, commandType: CommandType.Text)).ToList();
    }

    public async Task<TRes> QueryAsync<TRes, T>(string command, T parameters)
    {
        using IDbConnection connection = new MySqlConnection(_configuration.GetConnectionString("MySql"));
        return await connection.QuerySingleOrDefaultAsync<TRes>(command, parameters, commandType: CommandType.Text);
    }

    public async Task ExecuteAsync<T>(string command, T parameters)
    {
        using IDbConnection connection = new MySqlConnection(_configuration.GetConnectionString("MySql"));
        await connection.ExecuteAsync(command, parameters, commandType: CommandType.Text);
    }
}