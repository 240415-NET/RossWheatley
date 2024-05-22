using TBG;
using TBG.Logic;


namespace Project01.Test;

public class Project01DataTests
{
    [Theory]
    [InlineData("Ross")]
    public void UserExists_ExistingUser_ReturnTrue(string userName)
    {
        User user = Session.DataAccess.GetUser(userName);
        bool result = user != null;
        Assert.True(result);
    }

    [Theory]
    [InlineData("JohnJacobJingleheimerSchmidt")]
    public void UserExists_NonExistingUser_ReturnFalse(string userName)
    {
        User user = Session.DataAccess.GetUser(userName);
        bool result = user == null;
        Assert.False(result);
    }
}