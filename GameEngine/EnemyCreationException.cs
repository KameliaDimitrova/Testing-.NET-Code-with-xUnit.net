namespace GameEngine;
public class EnemyCreationException : Exception
{
    public string CustomMessage { get; private set; }

    public EnemyCreationException(string name)
    {
        this.CustomMessage = $"{name} is not a valid name for a Boss enemy, Boss enemy names must end with King or Queen";
    }
}