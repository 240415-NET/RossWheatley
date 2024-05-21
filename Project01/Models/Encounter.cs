namespace TBG;

public class Encounter
{
    public Guid SaveId { get; set; }
    public Guid EncounterId { get; set; }
    public Coord Coordinates { get; set; }

    public Encounter()
    {
        EncounterId = Guid.NewGuid();
    }

    public Encounter(Guid saveId, Guid encounterId, Coord coordinates)
    {
        SaveId = saveId;
        EncounterId = encounterId;
        Coordinates = coordinates;
    }
}