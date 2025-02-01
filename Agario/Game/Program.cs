using Agario;
using Engine;

class Program
{
    static void Main(string[] args)
    {
        var config = ConfigLoader.LoadConfig("Roboto_Regular.json");

        GameLoop gameLoop = new GameLoop(config);
        gameLoop.Run();
    }
}