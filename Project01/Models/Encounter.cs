namespace TBG;

public class Encounter
{
    public Guid SaveId { get; set; }
    public Guid EncounterId { get; set; }
    public Coord Coordinates { get; set; }
    public int EnemyLevel { get; set; }
    public int PlayerDamage { get; set; }

    public Encounter()
    {
        EncounterId = Guid.NewGuid();
        EnemyLevel = 0;
        PlayerDamage = 0;
    }

    public Encounter(Guid saveId, Guid encounterId, Coord coordinates)
    {
        SaveId = saveId;
        EncounterId = encounterId;
        Coordinates = coordinates;
        EnemyLevel = 0;
        PlayerDamage = 0;
    }
}