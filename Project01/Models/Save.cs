using System.Text.RegularExpressions;

namespace TBG;

public class Save
{
    public Guid SaveId { get; set; }
    public Guid UserId { get; set; }
    public GameObject PlayerObject { get; set; }
    public DateTime SaveDate { get; set; }
    public int Turns { get; set; }
    public int Units { get; set; }
    public List<int> Tasks { get; set; } // board positions where tasks are populated
    public List<int> Encounters { get; set; } // board positions where encounters occur
    public (int x, int y) GridConstraints { get; set; }

    public Save() { }
    public Save(User user, GameObject player)
    {
        SaveId = Guid.NewGuid();
        UserId = user.UserId;
        PlayerObject = player;
        SaveDate = DateTime.Now;
        Turns = 25;
        Units = 2;
        SetGridConstraints(Turns);
    }

    (int x, int y) SetGridConstraints(int turns)
    {
        return ((int)Math.Ceiling((double)turns / 2), (int)Math.Ceiling((double)turns / 2));
    }
}