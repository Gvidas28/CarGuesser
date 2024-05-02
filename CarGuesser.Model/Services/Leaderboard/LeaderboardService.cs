using CarGuesser.Model.Entities.Internal;
using CarGuesser.Model.Repositories.Leaderboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Leaderboard;

public class LeaderboardService : ILeaderboardService
{
    private readonly ILeaderboardRepository _leaderboardRepository;

    public LeaderboardService(
        ILeaderboardRepository leaderboardRepository
        )
    {
        _leaderboardRepository = leaderboardRepository;
    }

    public async Task AddNewLeaderboardRecordAsync(LeaderboardRecord leaderboard) => await _leaderboardRepository.InsertAsync(leaderboard);

    public async Task<List<LeaderboardRecord>> GetLeadeboardAsync() => await _leaderboardRepository.GetAsync();
}