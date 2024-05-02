using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Requests;
using CarGuesser.Server.Model.Entities.Responses;
using CarGuesser.Server.Model.Processors.Guess;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarGuesser.Server.React.Controllers;

[ApiController, Route("guess")]
public class GuessController : Controller
{
    private readonly IGuessProcessor _guessProcessor;

    public GuessController(
        IGuessProcessor guessProcessor
        )
    {
        _guessProcessor = guessProcessor;
    }

    [HttpGet, Route("get")]
    public async Task<ServerResult<GuessDataResponse>> GetGuessDataAsync() => await _guessProcessor.GetGuessDataAsync();

    [HttpPost, Route("submit")]
    public async Task<ServerResult<SubmitGuessResponse>> SubmitGuessAsync([FromBody]SubmitGuessRequest request) => await _guessProcessor.SubmitGuessAsync(request);
}