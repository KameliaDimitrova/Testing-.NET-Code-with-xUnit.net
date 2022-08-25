namespace GameEngine;
public class EnemyFactory
{
    public Enemy Create(string name, bool isBoss = false)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (isBoss)
        {
            if (!this.IsValidBossName(name))
            {
                throw new EnemyCreationException(name);
            }

            return new BossEnemy { Name = name };
        }
        return new NormalEnemy { Name = name };
    }

    private bool IsValidBossName(string name)
        => name.EndsWith("King") || name.EndsWith("Queen");
}
