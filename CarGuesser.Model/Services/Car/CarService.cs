using CarGuesser.Model.Entities.Enums;
using CarGuesser.Model.Entities.Exceptions;
using CarGuesser.Model.Entities.External;
using CarGuesser.Model.Entities.Internal;
using CarGuesser.Model.Helpers;
using CarGuesser.Model.Services.Client;
using CarGuesser.Model.Services.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuesser.Model.Services.Car;

public class CarService : ICarService
{
    private readonly IClientService _clientService;
    private readonly IImageService _imageService;

    public CarService(
        IClientService clientService,
        IImageService imageService
        )
    {
        _clientService = clientService;
        _imageService = imageService;
    }

    public async Task<List<CarModel>> GetRandomCarsAsync(Difficulty difficulty)
    {
        var randomYear = UtilityHelper.GetRandomNumber(1980, 2022);
        var carCount = GetCarCount(difficulty);

        var availableCarMakes = await _clientService.SendRequestAsync<GetMakesResponse>(new() { { "cmd", "getMakes" }, { "year", randomYear.ToString() } });
        if (availableCarMakes?.Makes?.Any() != true)
            throw new InternalException($"No available car makes found for the year {randomYear}!");

        var randomMake = availableCarMakes.Makes.OrderBy(_ => UtilityHelper.GetRandomNumber(1, 1000)).FirstOrDefault();

        var cars = await _clientService.SendRequestAsync<GetTrimsResponse>(new() { { "cmd", "getTrims" }, { "make", randomMake.MakeId }, { "year", randomYear.ToString() } });
        if (cars?.Trims?.Any() != true)
            throw new InternalException($"No car trims found for model {randomMake.MakeId}!");

        var randomCars = cars.Trims.OrderBy(_ => UtilityHelper.GetRandomNumber(1, 1000)).Take(carCount).ToList();
        return randomCars.Select(x => new CarModel
        {
            Id = x.ModelId,
            Make = x.MakeDisplay,
            Model = x.ModelName,
            Year = x.ModelYear,
            EnginePosition = x.ModelEnginePosition,
            EngineType = x.ModelEngineType,
            Doors = x.ModelDoors,
            Drive = x.ModelDrive,
            Trim = x.ModelTrim,
            Country = x.MakeCountry,
            TopSpeed = x.MaxTopSpeed,
            HorsePower = x.ModelEnginePowerPs,
            Transmission = x.ModelTransmissionType,
            ImageUrl = _imageService.GetFirstGoogleImageUrl($"{x.MakeDisplay} {x.ModelName} {x.ModelYear} {x.ModelTrim}")
        }).ToList();
    }

    private int GetCarCount(Difficulty difficulty) => difficulty switch
    {
        Difficulty.Easy => 2,
        Difficulty.Normal => 4,
        Difficulty.Hard => 6,
        _ => throw new ArgumentOutOfRangeException(nameof(Difficulty), "Unknown difficulty!")
    };
}