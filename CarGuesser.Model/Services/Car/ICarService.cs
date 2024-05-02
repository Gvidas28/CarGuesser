using CarGuesser.Model.Entities.Enums;
using CarGuesser.Model.Entities.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Car;

public interface ICarService
{
    Task<List<CarModel>> GetRandomCarsAsync(Difficulty difficulty);
}