using Agario;
using Engine;

class Program
{
    static void Main(string[] args)
    {
        var config = ConfigLoader.LoadConfig("Roboto_Regular.json");

        IGameRules agarioGame = new GameScene();
        GameLoop gameLoop = new GameLoop(config, agarioGame);
        gameLoop.Run();
    }
}