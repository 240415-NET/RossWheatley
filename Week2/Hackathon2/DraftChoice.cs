namespace Hackathon2;

public class DraftChoice
{
    private int round;
    private int pick;
    private string? team;
    private string? school;
    private string? player;
    private string? position;

    // Default constructor with default values
    public DraftChoice()
    {

    }

    // Overloaded constructor with all values passed as args
    public DraftChoice(int round, int pick, string? team, string? player, string? position, string? school)
    {
        this.round = round;
        this.pick = pick;
        Team = team;
        School = school;
        Player = player;
        Position = position;
    }

    public int Round
    {
        get { return round; }
        set { round = value; }
    }

    public int Pick
    {
        get { return pick; }
        set { pick = value; }
    }

    public string? Team
    {
        get { return team; }
        set { team = value; }
    }

    public string? School
    {
        get { return school; }
        set { school = value; }
    }
    public string? Player
    {
        get { return player; }
        set { player = value; }
    }
    public string? Position
    {
        get { return position; }
        set { position = value; }
    }

    public override string ToString()
    {
        return $"R{Round.ToString().PadLeft(2, '0')}P{Pick.ToString().PadLeft(2, '0')}. {Team} selects {Player}, {Position}, out of {School}";
    }
}
