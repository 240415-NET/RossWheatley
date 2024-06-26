using Microsoft.AspNetCore.Mvc;
using Practice.Entities;

namespace Practice.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService itemService;

    public ItemController(IItemService itemService)
    {
        this.itemService = itemService;
    }

    [HttpPost("{name}")]
    public async Task<ActionResult<Item>> PostNewUser(string name)
    {
        Item newItem = new Item(name);
        try
        {
            await itemService.CreateNewItemAsync(newItem);

            return Ok(newItem);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public double GetNumber(string s)
    {
        double dees = 34;
        return dees;
    }

    public int GetNumber(int d) {
        int i = 1;
        return i;
    }
}