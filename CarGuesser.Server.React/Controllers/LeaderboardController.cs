using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Models;
using CarGuesser.Server.Model.Entities.Requests;
using CarGuesser.Server.Model.Processors.Leaderboard;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Server.React.Controllers;

[ApiController, Route("leaderboard")]
public class LeaderboardController : Controller
{
    private readonly ILeaderboardProcessor _leaderboardProcessor;

    public LeaderboardController(
        ILeaderboardProcessor leaderboardProcessor
        )
    {
        _leaderboardProcessor = leaderboardProcessor;
    }

    [HttpPost("add")]
    public async Task<ServerResult> AddToLeaderboardAsync(AddToLeaderboardRequest request) => await _leaderboardProcessor.AddToLeaderboardAsync(request);

    [HttpGet("get")]
    public async Task<ServerResult<List<Leader>>> GetLeaderboardAsync() => await _leaderboardProcessor.GetLeaderboardAsync();
}