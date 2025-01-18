using Agario;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Game
{
    public class GameScene
    {
        private Player _player;
        private List<Food> _foods;
        private List<Enemy> _enemies;
        private Random _random;
        private RenderWindow _window;
        private Clock _clock;
        public static float deltaTime;

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

            for (int i = 0; i < 5; i++) 
            {
                SpawnEnemy();
            }

            _window = new RenderWindow(new VideoMode(1600, 1200), "Agar.io Clone");
            _clock = new Clock();
            _window.Closed += (sender, e) => _window.Close();
        }

        public void Run()
        {
            while (_window.IsOpen)
            {
                 deltaTime = _clock.Restart().AsSeconds();

                _window.DispatchEvents();

                _player.HandleInput();  
                _player.Update(deltaTime);
                Update(deltaTime);

                _window.Clear(Color.White);
                Render();
                _window.Display();
            }
        }

        private void Update(float deltaTime)
        {
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
                        _enemies[i].Grow();
                        _player.Reset();
                        return;
                    }
                }
                else
                {
                    _enemies[i].Update(_enemies, _player); 
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
        }

        private void Render()
        {
            _window.Draw(_player.Shape);

            foreach (var food in _foods)
                _window.Draw(food.Shape);

            foreach (var enemy in _enemies)
                _window.Draw(enemy.Shape);
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