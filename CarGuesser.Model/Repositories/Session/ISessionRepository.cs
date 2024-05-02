using CarGuesser.Model.Entities.Internal;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories.Session;

public interface ISessionRepository
{
    Task InsertAsync(SessionRecord session);
    Task<SessionRecord> GetAsync(string sessionId);
    Task UpdateAsync(SessionRecord session);
}