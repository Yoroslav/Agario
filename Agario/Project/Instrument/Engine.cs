using SFML.Graphics;
using SFML.Window;

namespace Agario.Instrument
{
    public class GameWindow
    {
        private RenderWindow _window;

        public GameWindow(uint width, uint height, string title)
        {
            _window = new RenderWindow(new VideoMode(width, height), title);
            _window.SetFramerateLimit(60);
            _window.Closed += (s, e) => _window.Close();
        }

        public bool IsOpen => _window.IsOpen;

        public void Clear(Color color) => _window.Clear(color);
        public void Display() => _window.Display();
        public void Draw(Drawable drawable) => _window.Draw(drawable);
        public void DispatchEvents() => _window.DispatchEvents();
    }

    public abstract class Scene
    {
        public abstract void Input();
        public abstract void Update(float deltaTime);
        public abstract void Render(GameWindow window);
    }
}