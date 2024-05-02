using CarGuesser.Model.Entities.Internal;
using CarGuesser.Model.Helpers;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories.Guess;

public class GuessRepository : IGuessRepository
{
    private readonly IRepository _repository;

    public GuessRepository(
        IRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<int> InsertAsync(GuessRecord guess)
    {
        var sql = $@"
                INSERT INTO `guesses`
                (`Answer`, `SessionId`)
                VALUES (@Answer, @SessionId);
                SELECT LAST_INSERT_ID();
                ";

        return await _repository.QueryAsync<int, dynamic>(sql, guess.MapToDynamic());
    }

    public async Task<GuessRecord> GetAsync(int id)
    {
        var sql = $@"
                SELECT `Id`, `Answer`, `SessionId`
                FROM `guesses`
                WHERE `Id` = @Id;
                ";

        return await _repository.QueryAsync<GuessRecord, dynamic>(sql, new { Id = id });
    }
}