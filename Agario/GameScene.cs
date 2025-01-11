using Agario;
using Engine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Game
{
    public class GameScene : Scene
    {
        private Player _player;
        private List<Food> _foods;
        private Random _random;

        public GameScene()
        {
            _player = new Player(new Vector2f(400, 300));
            _foods = new List<Food>();
            _random = new Random();

            for (int i = 0; i < 50; i++)
            {
                SpawnFood();
            }
        }

        private void SpawnFood()
        {
            var position = new Vector2f(_random.Next(0, 800), _random.Next(0, 600));
            _foods.Add(new Food(position));
        }

        public override void Input()
        {
            _player.HandleInput();
        }

        public override void Update(float deltaTime)
        {
            _player.Update(deltaTime);

            for (int i = _foods.Count - 1; i >= 0; i--)
            {
                if (_player.CheckCollision(_foods[i]))
                {
                    _foods.RemoveAt(i);
                    _player.Grow();
                    SpawnFood();
                }
            }
        }

        public override void Render(GameWindow window)
        {
            foreach (var food in _foods)
                window.Draw(food.Shape);

            window.Draw(_player.Shape);
        }
    }
}