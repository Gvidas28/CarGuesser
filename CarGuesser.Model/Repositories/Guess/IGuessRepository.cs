using CarGuesser.Model.Entities.Internal;
using System.Threading.Tasks;

namespace CarGuesser.Model.Repositories.Guess;

public interface IGuessRepository
{
    Task<int> InsertAsync(GuessRecord guess);
    Task<GuessRecord> GetAsync(int id);
}