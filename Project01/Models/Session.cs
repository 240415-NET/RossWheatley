using System.Reflection.Metadata;

namespace TBG;

public class Session
{
    public User ActiveUser { get; set; }
    public Save ActiveSave { get; set; }

    public Session() { }
}