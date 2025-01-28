using SFML.System;

namespace Agario
{
    public interface ICommand
    {
        void Execute();
    }

    public class MoveCommand : ICommand
    {
        private Player _player;
        private Vector2f _direction;

        public MoveCommand(Player player, Vector2f direction)
        {
            _player = player;
            _direction = direction;
        }

        public void Execute()
        {
            _player.Move(_direction);
        }
    }

    public class SwapCommand : ICommand
    {
        private Player _player;
        private List<Enemy> _enemies;

        public SwapCommand(Player player, List<Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
        }

        public void Execute()
        {
            _player.SwapWithClosestEnemy(_enemies);
        }
    }

    public class DefeatCommand : ICommand
    {
        private Player _player;

        public DefeatCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            _player.MarkAsDefeated();
        }
    }
}
