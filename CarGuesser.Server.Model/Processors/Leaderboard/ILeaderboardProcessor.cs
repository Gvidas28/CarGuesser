using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Models;
using CarGuesser.Server.Model.Entities.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Server.Model.Processors.Leaderboard;

public interface ILeaderboardProcessor
{
    Task<ServerResult> AddToLeaderboardAsync(AddToLeaderboardRequest request);
    Task<ServerResult<List<Leader>>> GetLeaderboardAsync();
}