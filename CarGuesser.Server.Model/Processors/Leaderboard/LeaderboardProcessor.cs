using CarGuesser.Model.Services.Leaderboard;
using CarGuesser.Model.Services.Session;
using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Models;
using CarGuesser.Server.Model.Entities.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuesser.Server.Model.Processors.Leaderboard;

public class LeaderboardProcessor : ILeaderboardProcessor
{
    private readonly ILogger<LeaderboardProcessor> _logger;
    private readonly ISessionService _sessionService;
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardProcessor(
        ILogger<LeaderboardProcessor> logger,
        ISessionService sessionService,
        ILeaderboardService leaderboardService
        )
    {
        _logger = logger;
        _sessionService = sessionService;
        _leaderboardService = leaderboardService;
    }

    public async Task<ServerResult> AddToLeaderboardAsync(AddToLeaderboardRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return new() { Success = false, Message = "Name cannot be empty!" };

            var session = await _sessionService.GetSessionAsync(request.SessionId);
            if (session is null)
                return new() { Success = false, Message = "Unknown session!" };

            await _leaderboardService.AddNewLeaderboardRecordAsync(new()
            {
                Name = request.Name,
                Difficulty = session.Difficulty,
                Score = session.Score
            });

            return new() { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(AddToLeaderboardAsync)}: {ex.Message}");
            return new() { Success = false, Message = "Technical error occured!" };
        }
    }

    public async Task<ServerResult<List<Leader>>> GetLeaderboardAsync()
    {
        try
        {
            var leaderboard = await _leaderboardService.GetLeadeboardAsync();
            var data = leaderboard.Select(x => new Leader
            {
                Name = x.Name,
                Difficulty = x.Difficulty.ToString(),
                Score = x.Score
            }).ToList();

            return new() { Success = true, Data = data };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(GetLeaderboardAsync)}: {ex.Message}");
            return new() { Success = false, Message = "Technical error occured!" };
        }
    }
}