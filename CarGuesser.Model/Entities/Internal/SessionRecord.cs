using CarGuesser.Model.Entities.Enums;

namespace CarGuesser.Model.Entities.Internal;

public class SessionRecord
{
    public int Id { get; set; }
    public string SessionId { get; set; }
    public Difficulty Difficulty { get; set; }
    public int RemainingLives { get; set; }
    public int Score { get; set; }
}