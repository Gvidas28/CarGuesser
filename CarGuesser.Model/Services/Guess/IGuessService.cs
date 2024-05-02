using CarGuesser.Model.Entities.Internal;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Guess;

public interface IGuessService
{
    Task<int> SaveGuessAsync(GuessRecord guess);
    Task<GuessRecord> GetGuessAsync(int id);
}