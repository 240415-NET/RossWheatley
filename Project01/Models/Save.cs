namespace TBG;

public class Save
{
    public Guid gameId { get; set; }
    public Guid userId { get; set; }
    public GameObject playerObject { get; set; }
    public int turns { get; set; }
    public int units { get; set; }

    public Save()
    {
        gameId = Guid.NewGuid();
        turns = 25;
        units = 2;
    }
}