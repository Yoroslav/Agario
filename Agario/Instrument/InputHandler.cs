using SFML.System;
using SFML.Window;

namespace Agario
{
    public class InputHandler
    {
        private Dictionary<Keyboard.Key, ICommand> _keyBindings;

        public InputHandler(Player player, List<Enemy> enemies)
        {
            _keyBindings = new Dictionary<Keyboard.Key, ICommand>
        {
            { Keyboard.Key.W, new MoveCommand(player, new Vector2f(0, -1)) },
            { Keyboard.Key.S, new MoveCommand(player, new Vector2f(0, 1)) },
            { Keyboard.Key.A, new MoveCommand(player, new Vector2f(-1, 0)) },
            { Keyboard.Key.D, new MoveCommand(player, new Vector2f(1, 0)) },
            { Keyboard.Key.F, new SwapCommand(player, enemies) },
            { Keyboard.Key.Q, new DefeatCommand(player) }
        };
        }

        public void HandleInput()
        {
            foreach (var keyBinding in _keyBindings)
            {
                if (Keyboard.IsKeyPressed(keyBinding.Key))
                {
                    keyBinding.Value.Execute();
                }
            }
        }
    }

}
