using CarGuesser.Model.Entities.Internal;
using CarGuesser.Model.Repositories.Guess;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Guess;

public class GuessService : IGuessService
{
    private readonly IGuessRepository _guessRepository;

    public GuessService(
        IGuessRepository guessRepository
        )
    {
        _guessRepository = guessRepository;
    }

    public async Task<int> SaveGuessAsync(GuessRecord guess) => await _guessRepository.InsertAsync(guess);

    public async Task<GuessRecord> GetGuessAsync(int id) => await _guessRepository.GetAsync(id);
}