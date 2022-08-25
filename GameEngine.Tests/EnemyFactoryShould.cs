namespace GameEngine.Tests;

[Trait("Category", "Enemy")] // Info When we want to categorize some tests. Here we categorize all tests in this class
public class EnemyFactoryShould
{
    [Fact]
    public void CreateNormalEnemyByDefault()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Act
        Enemy enemy = sut.Create("Zombie");

        //Assert
        Assert.IsType<NormalEnemy>(enemy);
    }

    [Fact(Skip = "Don`t need to run this")] //Info When we want to skip test
    public void CreateNormalEnemyByDefault_NotTypeExample()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Act
        Enemy enemy = sut.Create("Zombie");

        //Assert
        Assert.IsNotType<DateTime>(enemy);
    }
    [Fact]
    public void CreateBossEnemyByDefault_NotTypeExample()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Act
        Enemy enemy = sut.Create("BossName King", true);

        //Assert
        Assert.IsType<BossEnemy>(enemy);
    }

    [Fact]
    public void CreateBossEnemyByDefault_AssertAssignableTypes()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Act
        Enemy enemy = sut.Create("BossName Queen", true);

        //Assert
        //Assert.IsType<Enemy>(enemy); It's strict comparison between types
        Assert.IsAssignableFrom<Enemy>(enemy); //If we want to include derived type, we use IsAssignableFrom
    }

    [Fact] 
    public void CreateBossEnemyByDefault_CastReturnedTypeExample()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Act
        Enemy enemy = sut.Create("BossName King", true);

        //Assert
        BossEnemy boss= Assert.IsType<BossEnemy>(enemy);

        //Additional assert
        Assert.Equal(enemy.Name, boss.Name);
    }

    [Fact]
    public void CreateSepareteInstances()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Act
        Enemy enemy1 = sut.Create("Zombie");
        Enemy enemy2 = sut.Create("Zombie");

        //Assert
        Assert.NotSame(enemy1, enemy2);
    }

    [Fact]
    public void NotAllowNullName()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Assert
        //Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        Assert.Throws<ArgumentNullException>("name", () => sut.Create(default(string)));
    }

    [Fact]
    public void OnlyAllowKingOrQueenBossEnemies()
    {
        //Arange
        EnemyFactory sut = new EnemyFactory();

        //Assert
        EnemyCreationException ex =
            Assert.Throws<EnemyCreationException>(() => sut.Create("InvalidBossName", true));

        Assert.Equal($"InvalidBossName is not a valid name for a Boss enemy, Boss enemy names must end with King or Queen", ex.CustomMessage);

    }
}
