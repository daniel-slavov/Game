namespace Game.Data.Models;

public class GameRule
{
    public int Id { get; set; }
    public int ChoiceId { get; set; }
    public int WinAgainstId { get; set; }

    public Choice Choice { get; set; }
    public Choice WinAgainst { get; set; }
}