using CarGuesser.Model.Entities.Enums;
using CarGuesser.Model.Helpers;
using CarGuesser.Model.Services.Session;
using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CarGuesser.Server.Model.Processors.Session;

public class SessionProcessor : ISessionProcessor
{
    private readonly ILogger<SessionProcessor> _logger;
    private readonly ISessionService _sessionService;

    public SessionProcessor(
        ILogger<SessionProcessor> logger,
        ISessionService sessionService
        )
    {
        _logger = logger;
        _sessionService = sessionService;
    }

    public async Task<ServerResult<string>> CreateSessionAsync(CreateSessionRequest request)
    {
        try
        {
            if (!Enum.TryParse(request.Difficulty, out Difficulty difficulty))
                return new() { Success = false, Message = "Unknown difficulty!" };

            var sessionId = await _sessionService.CreateSessionAsync(difficulty);
            return new() { Success = true, Data = sessionId };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(CreateSessionAsync)} ({request.JsonToString()}): {ex}");
            return new() { Success = false, Message = "Technical error occured!" };
        }
    }
}