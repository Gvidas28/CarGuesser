using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories;

public interface IRepository
{
    Task<List<TRes>> QueryListAsync<TRes, T>(string command, T parameters);
    Task<TRes> QueryAsync<TRes, T>(string command, T parameters);
    Task ExecuteAsync<T>(string command, T parameters);
}