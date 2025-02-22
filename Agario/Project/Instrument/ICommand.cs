using Agario.Entities;
using SFML.System;
using System.Collections.Generic;

namespace Agario
{
    public interface ICommand
    {
        void Execute();
    }

    public class MoveCommand : ICommand
    {
        private Agario.Player _player;
        private Vector2f _direction;
        public MoveCommand(Agario.Player player, Vector2f direction)
        {
            _player = player;
            _direction = direction;
        }
        public void Execute() => _player.Move(_direction);
    }

    public class SwapCommand : ICommand
    {
        private Agario.Player _player;
        private List<Agario.Enemy> _enemies;
        public SwapCommand(Agario.Player player, List<Agario.Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
        }
        public void Execute() => _player.SwapWithClosestEnemy(_enemies);
    }

    public class DefeatCommand : ICommand
    {
        private Agario.Player _player;
        public DefeatCommand(Agario.Player player) => _player = player;
        public void Execute() => _player.MarkAsDefeated();
    }
}