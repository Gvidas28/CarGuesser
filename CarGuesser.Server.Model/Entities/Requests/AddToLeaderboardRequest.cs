namespace CarGuesser.Server.Model.Entities.Requests;

public class AddToLeaderboardRequest
{
    public string SessionId { get; set; }
    public string Name { get; set; }
}