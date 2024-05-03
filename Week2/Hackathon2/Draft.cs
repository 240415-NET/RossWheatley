namespace Hackathon2;

public class Draft
{
    public List<DraftChoice> Selections = new();
    private int round = 1;
    private int nextPick = 0;

    public void Counters(bool increment = true)
    {
        if (increment)
        {
            if (NextPick > 32)
            {
                Round++;
                NextPick = 1;
            }
            else
            {
                NextPick++;
            }
        }
        else
        {
            if (Round > 1 && NextPick == 1)
            {
                Round -= 1;
                NextPick = 32;
            }
            else
            {
                NextPick -= 1;
            }
        }
    }

    public int Round
    {
        get { return round; }
        private set { round = value; }
    }

    public int NextPick
    {
        get { return nextPick; }
        private set { nextPick = value; }
    }

}