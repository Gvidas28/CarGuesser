using CarGuesser.Model.Entities.Internal;
using CarGuesser.Model.Helpers;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories.Session;

public class SessionRepository : ISessionRepository
{
    private readonly IRepository _repository;

    public SessionRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task InsertAsync(SessionRecord session)
    {
        var sql = $@"
                INSERT INTO `sessions`
                (`SessionId`, `Difficulty`, `RemainingLives`, `Score`)
                VALUES (@SessionId, @Difficulty, @RemainingLives, @Score);
                ";

        await _repository.ExecuteAsync<dynamic>(sql, session.MapToDynamic());
    }

    public async Task<SessionRecord> GetAsync(string sessionId)
    {
        var sql = $@"
                SELECT `Id`, `SessionId`, `Difficulty`, `RemainingLives`, `Score`
                FROM `sessions`
                WHERE `SessionId` = @SessionId;
                ";

        return await _repository.QueryAsync<SessionRecord, dynamic>(sql, new { SessionId = sessionId });
    }

    public async Task UpdateAsync(SessionRecord session)
    {
        var sql = $@"
                UPDATE `sessions`
                SET `RemainingLives` = @RemainingLives, `Score` = @Score
                WHERE `Id` = @Id;
                ";

        await _repository.ExecuteAsync<dynamic>(sql, session.MapToDynamic());
    }
}