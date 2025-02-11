using Agario.Entities;
using Agario.Project.Game.Configs;
using Engine;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;

namespace Agario
{
    public class GameScene : IGameRules, IDisposable
    {
        private Player _player;
        private List<Food> _foods;
        private List<Enemy> _enemies;
        private Random _random;
        private InputHandler _inputHandler;
        private GameConfig _config;
        private SoundSystem _soundSystem;

        public GameScene(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
        }

        public void Initialize(GameConfig config)
        {
            _config = config;
            _player = new Player(
                Player.GetRandomPosition(_config.ScreenWidth, _config.ScreenHeight, 20),
                _config.PlayerSpeed,
                _config.PlayerGrowthFactor
            );

            _foods = new List<Food>();
            _enemies = new List<Enemy>();
            _random = new Random();
            _inputHandler = new InputHandler(_player, _enemies);

            LoadSounds();
            _soundSystem.PlaySound(AudioConfig.StartSound);

            InitializeEntities();
        }

        private void LoadSounds()
        {
            _soundSystem.LoadSound(AudioConfig.StartSound);
            _soundSystem.LoadSound(AudioConfig.EatFoodSound);
            _soundSystem.LoadSound(AudioConfig.EatEnemySound);
            _soundSystem.LoadSound(AudioConfig.PlayerDefeated);
        }

        private void InitializeEntities()
        {
            for (int i = 0; i < _config.InitialFoodCount; i++)
                SpawnFood();

            for (int i = 0; i < _config.MaxEnemies; i++)
                SpawnEnemy();
        }

        public void HandleInput()
        {
            _inputHandler.HandleInput();
        }

        public void Update(float deltaTime)
        {
            _player.Update(deltaTime);

            _foods.RemoveAll(food =>
            {
                if (_player.CheckCollision(food.Shape))
                {
                    HandlePlayerFoodCollision(_player, food);
                    SpawnFood();
                    return true;
                }
                return false;
            });

            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_player.CheckCollision(_enemies[i].Shape))
                {
                    HandlePlayerEnemyCollision(_player, _enemies[i]);
                    continue;
                }
                _enemies[i].Interact(_enemies, _foods, _player, deltaTime);
            }
        }

        private void HandlePlayerEnemyCollision(Player player, Enemy enemy)
        {
            if (player.IsLargerThan(enemy))
            {
                player.Grow();
                _enemies.Remove(enemy);
                SpawnEnemy();
                _soundSystem.PlaySound(AudioConfig.EatEnemySound);
            }
            else
            {
                enemy.Grow();
                player.MarkAsDefeated();
                _soundSystem.PlaySound(AudioConfig.PlayerDefeated);
            }
        }

        private void HandlePlayerFoodCollision(Player player, Food food)
        {
            player.Grow();
            _soundSystem.PlaySound(AudioConfig.EatFoodSound);
        }

        public void Render(RenderWindow window)
        {
            window.Clear(_config.BackgroundColor);
            window.Draw(_player.Shape);

            foreach (var food in _foods)
                window.Draw(food.Shape);

            foreach (var enemy in _enemies)
                window.Draw(enemy.Shape);
        }

        private void SpawnFood()
        {
            var position = new Vector2f(_random.Next(0, _config.ScreenWidth), _random.Next(0, _config.ScreenHeight));
            _foods.Add(new Food(position));
        }

        private void SpawnEnemy()
        {
            var position = new Vector2f(_random.Next(0, _config.ScreenWidth), _random.Next(0, _config.ScreenHeight));
            _enemies.Add(new Enemy(position, _config.EnemySpeed, _config.EnemyGrowthFactor, _config.EnemyAggression));
        }

        public void Dispose()
        {
            _soundSystem.Dispose();
        }
    }
}
