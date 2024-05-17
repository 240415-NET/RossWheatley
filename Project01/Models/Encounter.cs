namespace TBG;

public class Encounter
{
    public Guid EncounterId { get; set; }
    public Coord Coordinates { get; set; }

    public Encounter()
    {
        EncounterId = Guid.NewGuid();
    }
}