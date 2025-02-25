using Agario.Entities;
using Agario.Project.Game.Configs;
using Agario.Project.Game.MenuSkins;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Agario
{
    public class GameScene : IDisposable
    {
        private Player _player;
        private List<Food> _foods;
        private List<Enemy> _enemies;
        private Random _random;
        private GameConfig _config;
        private SoundSystem _soundSystem;

        public GameScene(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
        }

        public void Initialize(GameConfig config)
        {
            _config = config;

            Texture defaultSkin = ResourceManagerXXXXX.GetSkinTexture("default");
            if (defaultSkin == null)
                throw new InvalidOperationException("Failed to load default skin");

            _player = UnitFactory.CreatePlayer(
                Player.GetRandomPosition(_config.ScreenWidth, _config.ScreenHeight, 20),
                _config.PlayerSpeed,
                _config.PlayerGrowthFactor,
                defaultSkin,
                _config.ScreenWidth,
                _config.ScreenHeight
            );

            _foods = new List<Food>();
            _enemies = new List<Enemy>();
            _random = new Random();

            LoadSounds();
            _soundSystem.PlaySound("game_start");

            InitializeEntities();
        }

        private void LoadSounds()
        {
            _soundSystem.LoadSound("game_start");
            _soundSystem.LoadSound("eat_food");
            _soundSystem.LoadSound("eat_enemy");
            _soundSystem.LoadSound("player_defeated");
        }

        private void InitializeEntities()
        {
            for (int i = 0; i < _config.InitialFoodCount; i++)
                SpawnFood();

            for (int i = 0; i < _config.MaxEnemies; i++)
                SpawnEnemy();
        }

        public Player GetPlayer() => _player;
        public List<Enemy> GetEnemies() => _enemies;

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

            var foodToRemove = new List<Food>();
            foreach (var food in _foods)
            {
                if (_player.CheckCollision(food.Shape))
                {
                    HandlePlayerFoodCollision(_player, food);
                    SpawnFood();
                    foodToRemove.Add(food);
                }
            }
            foreach (var food in foodToRemove)
                _foods.Remove(food);

            var enemiesToRemove = new List<Enemy>();
            foreach (var enemy in _enemies)
            {
                if (_player.CheckCollision(enemy.Shape))
                {
                    HandlePlayerEnemyCollision(_player, enemy);
                    if (enemy.MarkedToKill)
                        enemiesToRemove.Add(enemy);
                }
            }
            foreach (var enemy in enemiesToRemove)
            {
                _enemies.Remove(enemy);
                SpawnEnemy();
            }
        }

        private void HandlePlayerEnemyCollision(Player player, Enemy enemy)
        {
            if (player.IsLargerThan(enemy))
            {
                player.Grow();
                _enemies.Remove(enemy);
                SpawnEnemy();
                if (_soundSystem.IsSoundLoaded("eat_enemy"))
                    _soundSystem.PlaySound("eat_enemy");
            }
            else
            {
                enemy.Grow();
                player.MarkAsDefeated();
                if (_soundSystem.IsSoundLoaded("player_defeated"))
                    _soundSystem.PlaySound("player_defeated");
            }
        }

        private void HandlePlayerFoodCollision(Player player, Food food)
        {
            player.Grow();
            if (_soundSystem.IsSoundLoaded("eat_food"))
                _soundSystem.PlaySound("eat_food");
        }

        private Texture LoadTextureFromResource(byte[] imageData)
        {
            return new Texture(imageData);
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
            _foods.Add(new Food(position, foodAnimator));
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

        public void Dispose()
        {
            _soundSystem.Dispose();
            foreach (var food in _foods)
                food.Animator.Dispose();
            foreach (var enemy in _enemies)
                enemy.Animator.Dispose();
        }
    }
}