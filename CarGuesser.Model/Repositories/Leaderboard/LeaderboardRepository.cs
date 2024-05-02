using CarGuesser.Model.Entities.Internal;
using CarGuesser.Model.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories.Leaderboard;

public class LeaderboardRepository : ILeaderboardRepository
{
	private readonly IRepository _repository;
	
	public LeaderboardRepository(
		IRepository repository
		)
	{
		_repository = repository;
	}

	public async Task InsertAsync(LeaderboardRecord leaderboard)
	{
		var sql = $@"
				INSERT INTO `leaderboard`
				(`Name`, `Difficulty`, `Score`)
				VALUES (@Name, @Difficulty, @Score);
				";

		await _repository.ExecuteAsync<dynamic>(sql, leaderboard.MapToDynamic());
	}

    public async Task<List<LeaderboardRecord>> GetAsync()
    {
        var sql = $@"
				SELECT `Id`, `Name`, `Difficulty`, `Score`
				FROM `leaderboard`;
				";

        return await _repository.QueryListAsync<LeaderboardRecord, dynamic>(sql, new { });
    }
}