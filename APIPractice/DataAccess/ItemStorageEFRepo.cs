using Practice.Entities;
using Practice.Services;
using Microsoft.EntityFrameworkCore;

namespace Practice.Data;

public class ItemStorageEFRepo : IItemStorageEFRepo
{
    private readonly PracticeContext context;
    public ItemStorageEFRepo(PracticeContext context)
    {
        this.context = context;
    }

    public async Task<Item> CreateItemInDBAsync(Item newItem)
    {
        context.Items.Add(newItem);

        await context.SaveChangesAsync();

        return newItem;
    }
}