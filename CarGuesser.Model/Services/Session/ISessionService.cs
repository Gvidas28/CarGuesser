using CarGuesser.Model.Entities.Enums;
using CarGuesser.Model.Entities.Internal;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Session;

public interface ISessionService
{
    Task<string> CreateSessionAsync(Difficulty difficulty);
    Task<SessionRecord> GetSessionAsync(string sessionId);
    Task UpdateSessionAsync(SessionRecord session);
}