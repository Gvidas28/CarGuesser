using CarGuesser.Model.Entities.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Leaderboard;

public interface ILeaderboardService
{
    Task AddNewLeaderboardRecordAsync(LeaderboardRecord leaderboard);
    Task<List<LeaderboardRecord>> GetLeadeboardAsync();
}