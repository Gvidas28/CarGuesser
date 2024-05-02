using CarGuesser.Model.Entities.Enums;
using CarGuesser.Model.Entities.Internal;
using CarGuesser.Model.Repositories.Session;
using System;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Session;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;

    public SessionService(
        ISessionRepository sessionRepository
        )
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<string> CreateSessionAsync(Difficulty difficulty)
    {
        var sessionId = Guid.NewGuid().ToString("N");
        await _sessionRepository.InsertAsync(new()
        {
            SessionId = sessionId,
            Difficulty = difficulty,
            RemainingLives = 3,
            Score = 0
        });

        return sessionId;
    }

    public async Task<SessionRecord> GetSessionAsync(string sessionId) => await _sessionRepository.GetAsync(sessionId);

    public async Task UpdateSessionAsync(SessionRecord session) => await _sessionRepository.UpdateAsync(session);
}