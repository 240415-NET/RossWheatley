 using TBG.Data;

namespace Project01.Test;

public class Project01DataTests
{
    [Theory]
    [InlineData("Ross")]
    public void UserExists_ExistingUser_ReturnTrue(string userName)
    {
        JSONFileData data = new();
        bool result = data.UserExists(userName);
        // bool result = true;
        Assert.True(result);
    }
    
    [Theory]
    [InlineData("Ross")]
    public void UserExists_NonExistingUser_ReturnFalse(string userName)
    {
        JSONFileData data = new();
        bool result = data.UserExists(userName);
        Assert.False(result);
    }
}