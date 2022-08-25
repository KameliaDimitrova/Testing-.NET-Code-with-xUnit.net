namespace GameEngine.Tests;
public class NonPlayerCharacterShould
{
    [Theory] //Data driven test 
    [InlineData(0, 100)] //Variant 1: Inline attribute <--> Local Developer
    [InlineData(1, 99)]
    [InlineData(50, 50)]
    [InlineData(101, 1)]
    public void TakeDamage(int damage, int expextedHealth)
    {
        NonPlayerCharacter sut = new NonPlayerCharacter();

        sut.TakeDamage(damage);

        Assert.Equal(expextedHealth, sut.Health);
    }

    [Theory] //Data driven test
    [MemberData(nameof(InternalHealthDamageTestData.TestData), 
        MemberType = typeof(InternalHealthDamageTestData))] //Variant 2: Propery/Field/Method data <--> Sharable Developer. In this case is used property data (TestData) from class (InternalHealthDamageTestData) 
    public void TakeDamageWithInternalClassData(int damage, int expextedHealth)
    {
        NonPlayerCharacter sut = new NonPlayerCharacter();
        sut.TakeDamage(damage);

        Assert.Equal(expextedHealth, sut.Health);
    }

    [Theory] //Data driven test
    [HealthDamageData] //Variant 3: CustomAttribute <--> Sharable Developer.
    public void TakeDamageWithAttributeData(int damage, int expextedHealth)
    {
        NonPlayerCharacter sut = new NonPlayerCharacter();
        sut.TakeDamage(damage);

        Assert.Equal(expextedHealth, sut.Health);
    }

    [Theory] //Data driven test
    [MemberData(nameof(ExternalHealthDamageTestData.TestData),
    MemberType = typeof(ExternalHealthDamageTestData))] //Variant 4: External data <--> Sharable. In this case is used external .csv file TestData.csv 
    public void TakeDamageWithExternalFileData(int damage, int expextedHealth)
    {
        NonPlayerCharacter sut = new NonPlayerCharacter();
        sut.TakeDamage(damage);

        Assert.Equal(expextedHealth, sut.Health);
    }
}
