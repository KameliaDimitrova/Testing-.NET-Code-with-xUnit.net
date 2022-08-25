namespace GameEngine.Tests;
public class GameStateFixture : IDisposable
{
    public GameState State { get; set; }

    public GameStateFixture()
    {
        State = new GameState();
    }

    public void Dispose()
    {
        //Cleanup;
    }
}
