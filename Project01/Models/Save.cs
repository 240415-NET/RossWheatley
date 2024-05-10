namespace TBG;

public class Save
{
    public Guid SaveId { get; set; }
    public Guid UserId { get; set; }
    public GameObject PlayerObject { get; set; }
    public DateTime SaveDate { get; set; }
    public int Turns { get; set; }
    public int Units { get; set; }

    public Save() { }
    public Save(User user, GameObject player)
    {
        SaveId = Guid.NewGuid();
        UserId = user.UserId;
        PlayerObject = player;
        SaveDate = DateTime.Now;
        Turns = 25;
        Units = 2;
    }
}