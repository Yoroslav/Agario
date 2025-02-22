using Agario;
using SFML.Graphics;

namespace Engine
{
    public interface IGameRules
    {
        void Initialize(Agario.Project.Game.Configs.GameConfig config);
        void HandleInput();
        void Update(float deltaTime);
        void Render(RenderWindow window);
    }
}