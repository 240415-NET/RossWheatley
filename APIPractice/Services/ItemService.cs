using Practice.Entities;

namespace Practice.Services;

public class ItemService : IItemService
{
    private readonly IItemStorageEFRepo itemStorage;

    public ItemService(IItemStorageEFRepo efRepo)
    {
        itemStorage = efRepo;
    }

    public async Task<Item> CreateNewItemAsync(Item newItem)
    {
        if (string.IsNullOrEmpty(newItem.Name) == true)
        {
            throw new Exception("Item name cannot be blank!");
        }

        await itemStorage.CreateItemInDBAsync(newItem);
        
        return newItem;
    }
}