using Agario;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine
{
    public class GameScene
    {
        private Player _player;
        private List<Food> _foods;
        private List<Enemy> _enemies;
        private Random _random;
        private InputHandler _inputHandler;

        public GameScene()
        {
            _player = new Player(Player.GetRandomPosition(1600, 1200, 20));
            _foods = new List<Food>();
            _enemies = new List<Enemy>();
            _random = new Random();
            _inputHandler = new InputHandler(_player, _enemies);

            for (int i = 0; i < 100; i++)
            {
                SpawnFood();
            }

            for (int i = 0; i < 5; i++)
            {
                SpawnEnemy();
            }
        }

        public void HandleInput()
        {
            _inputHandler.HandleInput();
        }

        public void Update(float deltaTime)
        {
            _player.Update(deltaTime);

            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_player.CheckCollision(_enemies[i].Shape))
                {
                    HandlePlayerEnemyCollision(_player, _enemies[i]);
                    continue;
                }

                _enemies[i].Interact(_enemies, _foods, _player, deltaTime);
            }

            for (int i = _foods.Count - 1; i >= 0; i--)
            {
                if (_player.CheckCollision(_foods[i].Shape))
                {
                    HandlePlayerFoodCollision(_player, _foods[i]);
                    _foods.RemoveAt(i);
                    SpawnFood();
                }
            }
        }

        private void HandlePlayerEnemyCollision(Player player, Enemy enemy)
        {
            if (player.IsLargerThan(enemy))
            {
                player.Grow();
                _enemies.Remove(enemy);
                SpawnEnemy();
            }
            else
            {
                enemy.Grow();
                player.MarkAsDefeated();
            }
        }

        private void HandlePlayerFoodCollision(Player player, Food food)
        {
            player.Grow();
        }

        public void Render(RenderWindow window)
        {
            window.Draw(_player.Shape);

            foreach (var food in _foods)
                window.Draw(food.Shape);

            foreach (var enemy in _enemies)
                window.Draw(enemy.Shape);
        }

        private void SpawnFood()
        {
            var position = new Vector2f(_random.Next(0, 1600), _random.Next(0, 1200));
            _foods.Add(new Food(position));
        }

        private void SpawnEnemy()
        {
            var position = new Vector2f(_random.Next(0, 1600), _random.Next(0, 1200));
            _enemies.Add(new Enemy(position));
        }
    }
}
