using Agario;
using SFML.Graphics;

namespace Engine
{
    public interface IGameRules
    {
        void Initialize(GameConfig config);
        void HandleInput();
        void Update(float deltaTime);
        void Render(RenderWindow window);
    }
}