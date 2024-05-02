namespace CarGuesser.Server.Model.Entities.Responses;

public class SubmitGuessResponse
{
    public bool GameOver { get; set; }
    public int TotalScore { get; set; }
}