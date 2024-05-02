namespace CarGuesser.Model.Entities.Internal;

public class GuessRecord
{
    public int Id { get; set; }
    public string Answer { get; set; }
    public string SessionId { get; set; }
}