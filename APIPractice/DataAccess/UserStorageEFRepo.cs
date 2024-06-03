using Practice.Entities;
using Practice.Services;
using Microsoft.EntityFrameworkCore;

namespace Practice.Data;

public class UserStorageEFRepo : IUserStorageEFRepo
{
    private readonly PracticeContext context;
    public UserStorageEFRepo(PracticeContext context)
    {
        this.context = context;
    }

    public async Task<User> CreateUserInDBAsync(User newUser)
    {

        context.Users.Add(newUser);

        await context.SaveChangesAsync();

        return new User();
    }
}