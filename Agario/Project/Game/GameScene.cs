using Agario.Entities;
using Engine;
using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class GameScene : IGameRules
    {
        private Player _player;
        private List<Food> _foods;
        private List<Enemy> _enemies;
        private Random _random;
        private InputHandler _inputHandler;
        private GameConfig _config;
        private SoundSystem _soundSystem;

        public void Initialize(GameConfig config)
        {
            _config = config;
            _player = new Player(Player.GetRandomPosition(_config.ScreenWidth, _config.ScreenHeight, 20), _config.PlayerSpeed, _config.PlayerGrowthFactor);
            _foods = new List<Food>();
            _enemies = new List<Enemy>();
            _random = new Random();
            _inputHandler = new InputHandler(_player, _enemies);
            _soundSystem = new SoundSystem();
            _soundSystem.LoadSound("game_start", "path_to_game_start_sound.wav");
            _soundSystem.LoadSound("eat_food", "path_to_eat_food_sound.wav");
            _soundSystem.LoadSound("eat_enemy", "path_to_eat_enemy_sound.wav");
            _soundSystem.LoadSound("player_defeated", "path_to_player_defeated_sound.wav");

            _soundSystem.PlaySound("game_start");

            for (int i = 0; i < _config.InitialFoodCount; i++)
            {
                SpawnFood();
            }

            for (int i = 0; i < _config.MaxEnemies; i++)
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
                _soundSystem.PlaySound("eat_enemy"); 
            }
            else
            {
                enemy.Grow();
                player.MarkAsDefeated();
                _soundSystem.PlaySound("player_defeated"); 
            }
        }

        private void HandlePlayerFoodCollision(Player player, Food food)
        {
            player.Grow();
            _soundSystem.PlaySound("eat_food");
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
    }
}