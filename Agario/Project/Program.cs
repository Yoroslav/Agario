using Agario;
using Agario.Project.Game.Configs;
using Engine;
using System.Resources;

class Program
{
    static void Main(string[] args)
    {
        var config = ConfigLoader.LoadConfig("Roboto_Regular.json");

        ResourceManager resourceManager = new ResourceManager("Agario.Sound", typeof(Program).Assembly);

        var audioManager = new SoundSystem(resourceManager);
        audioManager.LoadSound(AudioConfig.StartSound);
        audioManager.PlaySound(AudioConfig.StartSound);

        IGameRules agarioGame = new GameScene(audioManager);
        GameLoop gameLoop = new GameLoop(config, agarioGame);
        gameLoop.Run();
    }
}
