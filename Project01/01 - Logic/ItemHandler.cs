namespace TBG.Logic;

public static class ItemHandler
{
    public static Item GetSearchItem(bool roll = true)
    {
        Item item = Session.ActiveSave.Items.Find(item => item.ItemId == (Guid)ItemIdSearch()) ?? new Item();
        if (roll)
        {
            item = RandomizeItem(item);
        }
        DeleteItem();
        Session.ActiveSave.Items.Add(item);
        return item;
    }

    public static Guid? ItemIdSearch()
    {
        foreach (Item item in Session.ActiveSave.Items)
        {
            if (Session.ActiveSave.PlayerObject.GameObjectAtCoordinates(item.Coordinates.X, item.Coordinates.Y))
            {
                return item.ItemId;
            }
        }
        return null;
    }

    static Item RandomizeItem(Item item)
    {
        Random random = new();
        item.SkillIndex = random.Next(Session.ActiveSave.PlayerObject.Skills.Count());
        item.Modifier = ((random.Next(3) + 1) * Session.ActiveSave.PlayerObject.Level);
        return item;
    }

    public static void DeleteItem()
    {
        DeleteItem((Guid)ItemIdSearch());
    }

    public static void DeleteItem(Guid itemId)
    {
        Session.ActiveSave.Items.RemoveAll(item => item.ItemId == itemId);
    }
}