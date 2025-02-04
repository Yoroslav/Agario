using SFML.Graphics;

namespace Engine.Interfaces
{
    public interface IEntity
    {
        void Update(float deltaTime);
        void Draw(RenderTarget target);
    }
}