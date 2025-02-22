using Agario.Entities;
using Agario.Project.Game.Animations;
using Agario.Project.Game.Configs;
using Agario.Properties;
using Engine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;

namespace Agario
{
    public class GameScene : Engine.IGameRules, IDisposable
    {
        private Player _player;
        private List<Entities.Food> _foods;
        private List<Enemy> _enemies;
        private Random _random;
        private InputHandler _inputHandler;
        private GameConfig _config;
        private SoundSystem _soundSystem;
        private Texture _playerTexture;
        public GameScene(SoundSystem soundSystem, Texture playerTexture)
        {
            _soundSystem = soundSystem;
            _playerTexture = playerTexture;
        }
        public void Initialize(GameConfig config)
        {
            _config = config;
            _player = UnitFactory.CreatePlayer(Player.GetRandomPosition(_config.ScreenWidth, _config.ScreenHeight, 20),
                _config.PlayerSpeed, _config.PlayerGrowthFactor, _playerTexture);
            _foods = new List<Entities.Food>();
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
        public void HandleInput() => _inputHandler.HandleInput();
        public void Update(float deltaTime)
        {
            _player.Update(deltaTime);
            foreach (var food in _foods)
                food.Update(deltaTime);
            foreach (var enemy in _enemies)
            {
                enemy.Update(deltaTime);
                enemy.Interact(_enemies, _foods, _player, deltaTime);
            }
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
        private void HandlePlayerFoodCollision(Player player, Entities.Food food)
        {
            player.Grow();
            _soundSystem.PlaySound(AudioConfig.EatFoodSound);
        }
        private Texture LoadTextureFromResource(byte[] imageData)
        {
            using (var stream = new MemoryStream(imageData))
            {
                return new Texture(stream);
            }
        }
        public void Render(RenderWindow window)
        {
            window.Clear(_config.BackgroundColor);
            _player.Animator.Draw(window);
            foreach (var food in _foods)
                food.Draw(window);
            foreach (var enemy in _enemies)
                enemy.Draw(window);
        }
        private void SpawnFood()
        {
            var position = new Vector2f(
                _random.Next(0, _config.ScreenWidth),
                _random.Next(0, _config.ScreenHeight)
            );
            var foodAnimator = new Animator(
                texture: LoadTextureFromResource(Units.foodImage),
                frameWidth: 32,
                frameHeight: 32,
                totalFrames: 11,
                updateInterval: 0.1f
            );
            foodAnimator.SetScale(5f, 5f);
            _foods.Add(new Entities.Food(position, foodAnimator));
        }
        private void SpawnEnemy()
        {
            var position = new Vector2f(
                _random.Next(0, _config.ScreenWidth),
                _random.Next(0, _config.ScreenHeight)
            );
            var enemyAnimator = new Animator(
                texture: LoadTextureFromResource(Units.enemyImage),
                frameWidth: 32,
                frameHeight: 32,
                totalFrames: 11,
                updateInterval: 0.05f
            );
            enemyAnimator.SetScale(12.0f, 12.0f);
            _enemies.Add(new Enemy(
                position,
                _config.EnemySpeed,
                _config.EnemyGrowthFactor,
                _config.EnemyAggression,
                enemyAnimator
            ));
        }
        public void Dispose() => _soundSystem.Dispose();
    }
}