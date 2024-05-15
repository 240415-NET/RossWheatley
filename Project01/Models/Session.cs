using System.Reflection.Metadata;

namespace TBG.Logic;

public class Session
{
    public User ActiveUser { get; set; }
    public Save ActiveSave { get; set; }

    public Session() { }
}