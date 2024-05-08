namespace TBG;

public class Save
{
    public Guid gameId { get; set; }
    private GameObject playerObject;
    private int turns;
    private int units;

    public Save()
    {
        gameId = Guid.NewGuid();
    }
}