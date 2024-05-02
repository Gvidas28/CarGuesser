using CarGuesser.Model.Entities.Enums;

namespace CarGuesser.Model.Entities.Internal;

public class LeaderboardRecord
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Difficulty Difficulty { get; set; }
    public int Score { get; set; }
}