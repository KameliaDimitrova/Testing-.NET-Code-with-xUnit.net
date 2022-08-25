namespace GameEngine.Tests;
using Xunit.Abstractions;

public class PlayerCharacterShould : IDisposable
{
    private readonly PlayerCharacter _sut;
    private readonly ITestOutputHelper _output;

    public PlayerCharacterShould(ITestOutputHelper output)
    {
        this._output = output;

        this._output.WriteLine("Creating new PlayerCharacter");
        this._sut = new PlayerCharacter();
    }

    [Fact]
    public void BeInexperiencedWhenNew()
    {
        //Arange //_sut => system under test 

        //Assert
         Assert.True(_sut.IsNoob);
    }

    [Fact]
    public void CalculateFullName()
    {
        //Arange
        _sut.FirstName = "Sarah";
        _sut.LastName = "Smith";

        //Assert
        Assert.Equal("Sarah Smith", _sut.FullName);
    }

    [Fact]
    public void HaveFullNameStartingWithFirstName()
    {
        //Arange
        _sut.FirstName = "Sarah";
        _sut.LastName = "Smith";

        //Assert
        Assert.StartsWith("Sarah", _sut.FullName);
    }

    [Fact]
    public void HaveFullNameEndingWithLastName()
    {
        //Arange
        _sut.FirstName = "Sarah";
        _sut.LastName = "Smith";

        //Assert
        Assert.EndsWith("Smith", _sut.FullName);
    }

    [Fact]
    public void CalculateFullName_IgnoreCaseAssertExample()
    {
        //Arange
        _sut.FirstName = "SARAH";
        _sut.LastName = "SMITH";

        //Assert
        Assert.Equal("Sarah Smith", _sut.FullName, ignoreCase: true);
    }

    [Fact]
    public void CalculateFullName_SubstringExample()
    {
        //Arange
        _sut.FirstName = "Sarah";
        _sut.LastName = "Smith";

        //Assert
        Assert.Contains("ah Sm", _sut.FullName);
    }

    [Fact]
    public void CalculateFullNameWithTitleCase()
    {
        //Arange
        _sut.FirstName = "Sarah";
        _sut.LastName = "Smith";

        //Assert
        Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
    }

    [Fact]
    public void DefaultHealth_ShouldStartWithDefaultValue()
    {
        //Assert
        Assert.Equal(100, _sut.Health);
    }

    [Fact]
    public void DefaultHealth_ShouldNotStartWith0()
    {
        //Assert
        Assert.NotEqual(0, _sut.Health);
    }

    [Fact]
    public void IncreaseHealthAfterSleeping()
    {
        //Act
        _sut.Sleep(); //Expect increase between 1 to 100 inclusive

        //Assert
        //Assert.True(_sut.Health >= 101 && _sut.Health <= 200);
        Assert.InRange<int>(_sut.Health, 101, 200);
    }

    [Fact]
    public void NotHaveNickNameByDefault()
    {
        //Assert
        Assert.Null(_sut.Nickname);
    }

    [Fact]
    public void WeaponsShouldContainDefaultCollectionValues()
    {
        //Assert
        Assert.Contains("Long Bow", _sut.Weapons);
        Assert.Contains("Short Bow", _sut.Weapons);
        Assert.Contains("Short Sword", _sut.Weapons);
        Assert.Equal(3, _sut.Weapons?.Count());
    }

    [Fact]
    public void WeaponsShouldNotContainStaffOfWonder()
    {
        //Assert
        Assert.DoesNotContain("Staff of Wonder", _sut.Weapons);
    }

    [Fact]
    public void HaveAtLeastOneKindOfSword()
    {
        //Assert
        Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
    }

    [Fact]
    public void HaveAllExpectedWeapons()
    {
        var expectedWeapons = new[]
        {
            "Long Bow",
            "Short Bow",
            "Short Sword",
        };

        //Assert
        Assert.Equal(expectedWeapons, _sut.Weapons);
    }

    [Fact]
    public void HaveNoEmptyDefaultWeapons()
    {
        //Assert
        Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrEmpty(weapon)));
    }

    [Fact]
    public void RaiseSleptEvent()
    {
        //Assert
        Assert.Raises<EventArgs>(
            handler => _sut.PlayerSlept += handler,
            handler => _sut.PlayerSlept -= handler,
            () => _sut.Sleep());
    }

    [Fact]
    public void RaisePropertyChangeEvent()
    {
        //Assert
        Assert.PropertyChanged(_sut, "Health", () => _sut.TakeDamage(10));
    }

    [Fact]
    public void TakeZeroDamage()
    {
        _sut.TakeDamage(0);

        Assert.Equal(100, _sut.Health);
    }

    [Fact]
    public void TakeOneDamage()
    {
        _sut.TakeDamage(1);

        Assert.Equal(99, _sut.Health);
    }

    [Fact]
    public void TakeMediumDamage()
    {
        _sut.TakeDamage(50);

        Assert.Equal(50, _sut.Health);
    }

    [Fact]
    public void TakeMinimum1Damage()
    {
        _sut.TakeDamage(101);

        Assert.Equal(1, _sut.Health);
    }

    [Theory] //Data driven test 
    [InlineData(0, 100)] //Variant 1: Inline attribute <--> Local Developer
    [InlineData(1, 99)]
    [InlineData(50, 50)]
    [InlineData(101, 1)]
    public void TakeDamage(int damage, int expextedHealth)
    {
        _sut.TakeDamage(damage);

        Assert.Equal(expextedHealth, _sut.Health);
    }

    public void Dispose()
    {
        _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");

        //_sut.Dispose();
    }
}