namespace GameEngine.Tests;

using Xunit.Abstractions;
public class BossAnemyShould
{
    private readonly ITestOutputHelper _output;

    public BossAnemyShould(ITestOutputHelper output)
    {
        this._output = output;
    }

    [Fact]
    [Trait("Category", "Boss")]
    public void HaveCorrectPower()
    {
        _output.WriteLine("This test is for creating New Boss Enemy!");
        //Arange
        var sut = new BossEnemy();

        //Assert
        Assert.Equal(166.667, sut.SpecialAttackPower, 3);
    }
}
