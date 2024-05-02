using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Requests;
using CarGuesser.Server.Model.Processors.Session;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarGuesser.Server.React.Controllers;

[ApiController, Route("session")]
public class SessionController : Controller
{
    private readonly ISessionProcessor _sessionProcessor;

    public SessionController(
        ISessionProcessor sessionProcessor
        )
    {
        _sessionProcessor = sessionProcessor;
    }

    [HttpPost, Route("create")]
    public async Task<ServerResult<string>> CreateSessionAsync([FromBody] CreateSessionRequest request) => await _sessionProcessor.CreateSessionAsync(request);
}