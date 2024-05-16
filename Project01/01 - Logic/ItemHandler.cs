namespace TBG.Logic;

public static class ItemHandler
{
    public static bool Search()
    {
        foreach (Item item in Session.ActiveSave.Items)
        {
            if (Session.ActiveSave.PlayerObject.GameObjectAtCoordinates(item.Coordinates.X, item.Coordinates.Y))
            {
                return true;
            }
        }
        return false;
    }
}