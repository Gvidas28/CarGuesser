using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Requests;
using CarGuesser.Server.Model.Entities.Responses;
using System.Threading.Tasks;

namespace CarGuesser.Server.Model.Processors.Guess;

public interface IGuessProcessor
{
    Task<ServerResult<GuessDataResponse>> GetGuessDataAsync();
    Task<ServerResult<SubmitGuessResponse>> SubmitGuessAsync(SubmitGuessRequest request);
}