using CarGuesser.Model.Entities.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories.Leaderboard;

public interface ILeaderboardRepository
{
    Task InsertAsync(LeaderboardRecord leaderboard);
    Task<List<LeaderboardRecord>> GetAsync();
}