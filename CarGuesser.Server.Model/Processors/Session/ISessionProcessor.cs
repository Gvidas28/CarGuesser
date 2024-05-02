using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Requests;
using System.Threading.Tasks;

namespace CarGuesser.Server.Model.Processors.Session;

public interface ISessionProcessor
{
    Task<ServerResult<string>> CreateSessionAsync(CreateSessionRequest request);
}