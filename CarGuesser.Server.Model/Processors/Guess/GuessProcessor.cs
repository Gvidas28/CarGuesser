using CarGuesser.Model.Entities.Enums;
using CarGuesser.Model.Helpers;
using CarGuesser.Model.Services.Car;
using CarGuesser.Model.Services.Guess;
using CarGuesser.Model.Services.Session;
using CarGuesser.Server.Model.Entities;
using CarGuesser.Server.Model.Entities.Models;
using CarGuesser.Server.Model.Entities.Requests;
using CarGuesser.Server.Model.Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarGuesser.Server.Model.Processors.Guess;

public class GuessProcessor : IGuessProcessor
{
    private readonly ILogger<GuessProcessor> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICarService _carService;
    private readonly ISessionService _sessionService;
    private readonly IGuessService _guessService;

    public GuessProcessor(
        ILogger<GuessProcessor> logger,
        IHttpContextAccessor httpContextAccessor,
        ICarService carService,
        ISessionService sessionService,
        IGuessService guessService
        )
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _carService = carService;
        _sessionService = sessionService;
        _guessService = guessService;
    }

    public async Task<ServerResult<GuessDataResponse>> GetGuessDataAsync()
    {
        try
        {
            var queryParams = _httpContextAccessor.HttpContext?.Request?.Query;
            if (queryParams?.ContainsKey("sessionId") != true)
                return new() { Success = false, Message = "Missing sessionId parameter!" };

            var session = await _sessionService.GetSessionAsync(queryParams["sessionId"].ToString());
            if (session is null)
                return new() { Success = false, Message = "Unknown session!" };

            var cars = await _carService.GetRandomCarsAsync(session.Difficulty);

            var carToGuess = cars.OrderBy(_ => UtilityHelper.GetRandomNumber(1, 1000)).FirstOrDefault();
            var guessId = await _guessService.SaveGuessAsync(new() { Answer = carToGuess.Id, SessionId = session.SessionId });

            var guessOptions = cars.Select(x => new GuessOption
            {
                Id = x.Id,
                Make = x.Make,
                Model = x.Model,
                Year = x.Year,
                Trim = x.Trim,
                ImageUrl = x.ImageUrl
            }).ToList();

            var answerDescription = new AnswerDescription
            {
                EnginePosition = carToGuess.EnginePosition,
                EngineType = carToGuess.EngineType,
                HorsePower = carToGuess.HorsePower,
                Doors = carToGuess.Doors,
                Drive = carToGuess.Drive,
                Country = carToGuess.Country,
                TopSpeed = carToGuess.TopSpeed,
                Transmission = carToGuess.Transmission
            };

            return new()
            {
                Success = true,
                Data = new()
                {
                    GuessId = guessId,
                    Options = guessOptions,
                    Description = answerDescription,
                    RemainingLives = session.RemainingLives,
                    Score = session.Score
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(GetGuessDataAsync)}: {ex.Message}");
            return new() { Success = false, Message = "Technical error ocurred!" };
        }
    }

    public async Task<ServerResult<SubmitGuessResponse>> SubmitGuessAsync(SubmitGuessRequest request)
    {
        try
        {
            var session = await _sessionService.GetSessionAsync(request.SessionId);
            if (session is null)
                return new() { Success = false, Message = "Unknown session!" };

            if (session.RemainingLives < 1)
                return new() { Success = true, Data = new() { GameOver = true, TotalScore = session.Score } };

            var guess = await _guessService.GetGuessAsync(request.GuessId);
            if (guess is null)
                return new() { Success = false, Message = "Unknown guess!" };

            var correct = guess.Answer == request.Answer;

            if (correct)
                session.Score += DetermineReceivedScoreByDifficulty(session.Difficulty);
            else
                session.RemainingLives--;

            await _sessionService.UpdateSessionAsync(session);
            return new() { Success = true, Data = new() { GameOver = session.RemainingLives < 1, TotalScore = session.Score } };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(SubmitGuessAsync)}: {ex.Message}");
            return new() { Success = false, Message = "Technical error occured!" };
        }
    }

    private int DetermineReceivedScoreByDifficulty(Difficulty difficulty) => difficulty switch
    {
        Difficulty.Easy => 1,
        Difficulty.Normal => 2,
        Difficulty.Hard => 3,
        _ => throw new ArgumentOutOfRangeException(nameof(Difficulty), "Unknown difficulty!")
    };
}