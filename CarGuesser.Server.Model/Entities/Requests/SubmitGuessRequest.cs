namespace CarGuesser.Server.Model.Entities.Requests;

public class SubmitGuessRequest
{
    public int GuessId { get; set; }
    public string SessionId { get; set; }
    public string Answer { get; set; }
}