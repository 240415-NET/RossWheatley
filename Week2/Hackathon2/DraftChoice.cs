namespace Hackathon2;

public class DraftChoice
{
    private int round = 0;
    private int pick = 0;
    private string team = "Empty";
    private string school = "Empty";
    private string player = "Empty";
    private string position = "Empty";

    // Default constructor with default values
    public DraftChoice()
    {

    }

    // Overloaded constructor with all values passed as args
    public DraftChoice(int round, int pick, string team, string player, string position, string school)
    {
        this.round = round;
        this.pick = pick;
        this.team = team;
        this.player = player;
        this.position = position;
        this.school = school;
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

    public string Team
    {
        get { return team; }
        set { team = value; }
    }

    public string Player
    {
        get { return player; }
        set { player = value; }
    }
    public string Position
    {
        get { return position; }
        set { position = value; }
    }

    public string School
    {
        get { return school; }
        set { school = value; }
    }

    public override string ToString()
    {
        return $"R{Round}P{Pick}. {Team} selects {Player}, {Position}, out of {School}";
    }
} // End of DraftChoice Classtwit

public class Draft : List<DraftChoice>
{
    // Hello, World!
}

