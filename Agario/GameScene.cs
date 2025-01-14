using SFML.System;
using Engine;
using Agario;

namespace Game
{
    public class GameScene : Scene
    {
        private Player _player;
        private List<Food> _foods;
        private List<Enemy> _enemies;
        private Random _random;

        public GameScene()
        {
            _player = new Player(new Vector2f(800, 600));
            _foods = new List<Food>();
            _enemies = new List<Enemy>();
            _random = new Random();

            for (int i = 0; i < 100; i++)  
            {
                SpawnFood();
            }
            for (int i = 0; i < 10; i++)  
            {
                SpawnEnemy();
            }
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

        public override void Input() => _player.HandleInput();

        public override void Update(float deltaTime)
        {
            _player.Update(deltaTime);

            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_player.CheckCollision(_enemies[i]))
                {
                    if (_player.Shape.Radius > _enemies[i].Shape.Radius)
                    {
                        _player.Grow();
                        _enemies.RemoveAt(i);
                        SpawnEnemy();
                    }
                    else
                    {
                        _player.Reset();
                        return;
                    }
                }
            }

            for (int i = _foods.Count - 1; i >= 0; i--)
            {
                if (_player.CheckCollision(_foods[i]))
                {
                    _foods.RemoveAt(i);
                    _player.Grow();
                    SpawnFood();
                }
            }

            foreach (var enemy in _enemies)
            {
                enemy.Update();
            }
        }

        public override void Render(GameWindow window)
        {
            foreach (var food in _foods)
                window.Draw(food.Shape);

            foreach (var enemy in _enemies)
                window.Draw(enemy.Shape);

            window.Draw(_player.Shape);
        }
    }
}