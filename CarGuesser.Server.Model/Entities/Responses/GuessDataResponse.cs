using CarGuesser.Server.Model.Entities.Models;
using System.Collections.Generic;

namespace CarGuesser.Server.Model.Entities.Responses;

public class GuessDataResponse
{
    public int GuessId { get; set; }
    public int RemainingLives { get; set; }
    public int Score { get; set; }
    public AnswerDescription Description { get; set; }
    public List<GuessOption> Options { get; set; }
}