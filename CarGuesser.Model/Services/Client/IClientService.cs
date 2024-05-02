using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Client;

public interface IClientService
{
    Task<Response> SendRequestAsync<Response>(Dictionary<string, string> queryParams);
}