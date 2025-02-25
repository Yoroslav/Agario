using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Agario.Project.Game.MenuSkins
{
    public class SkinMenu
    {
        private RenderWindow _window;
        private List<UIButton> _skinButtons = new();
        private List<UIButton> _systemButtons = new();
        private Animator? _currentAnimator;
        private Texture _buttonTexture;
        private Font _font;
        private RectangleShape _background;
        private Text _title;

        public Texture? SelectedSkin { get; private set; }
        public event Action OnPlay;

        private UIButton _playButton;
        public SkinMenu(RenderWindow window)
        {
            _window = window;
            _window.Closed += (sender, e) => _window.Close();

            LoadResources();
            SetupUI();
            SetupSystemButtons();
        }

        private void LoadResources()
        {
            _buttonTexture = ResourceManagerXXXXX.GetUITexture("button_base");
            _font = ResourceManagerXXXXX.GetFont();
        }

        private void SetupUI()
        {
            _background = new RectangleShape(new Vector2f(_window.Size.X, _window.Size.Y))
            {
                FillColor = new Color(40, 40, 40)
            };

            _title = new Text("Skin Selector", _font, 48)
            {
                Position = new Vector2f(50, 30),
                FillColor = Color.White,
                OutlineColor = Color.Black,
                OutlineThickness = 2,
                Style = Text.Styles.Bold
            };

            LoadSkins();
        }

        private void LoadSkins()
        {
            string[] skinNames = { "cobrasrock", "Doen77", "Etho", "Hotch" };
            Vector2f startPos = new(50, 120);
            float buttonSpacing = 100;

            foreach (var name in skinNames)
            {
                string skinName = name;
                var button = new UIButton(
                    _buttonTexture,
                    _font,
                    skinName,
                    new Vector2f(startPos.X, startPos.Y + _skinButtons.Count * buttonSpacing),
                    () => SelectSkin(skinName), 
                    new Vector2f(2f, 2f)
                );
                _skinButtons.Add(button);
            }
        }

        private void SetupSystemButtons()
        {
            Vector2f startPos = new(50, 500);
            float spacing = 250;

            _systemButtons.Add(new UIButton(_buttonTexture, _font, "Download Skin", startPos, DownloadSkin, new Vector2f(2f, 2f)));
            _systemButtons.Add(new UIButton(_buttonTexture, _font, "Open Folder", startPos + new Vector2f(spacing, 0), OpenSkinFolder, new Vector2f(2f, 2f)));
            _systemButtons.Add(new UIButton(_buttonTexture, _font, "Exit", startPos + new Vector2f(spacing * 2, 0), () => _window.Close(), new Vector2f(2f, 2f)));

            _playButton = new UIButton(_buttonTexture, _font, "Play", startPos + new Vector2f(spacing * 3, 0), StartGame, new Vector2f(2f, 2f));
            _systemButtons.Add(_playButton);

            _playButton.SetEnabled(false);
        }    

        private void SelectSkin(string skinName)
        {
            SelectedSkin = ResourceManagerXXXXX.GetSkinTexture(skinName);

            if (SelectedSkin == null)
            {
                Console.WriteLine($"Skin '{skinName}' not found.");
                _playButton.SetEnabled(false); 
                return;
            }

            _currentAnimator = new Animator(
                SelectedSkin,
                frameWidth: 32,
                frameHeight: 64,
                totalFrames: 4,
                updateInterval: 0.15f
            );
            _currentAnimator.SetScale(8f, 8f);
            _currentAnimator.SetRow(0);
            _currentAnimator.IsMoving = true;
            _playButton.SetEnabled(true);
        }


        private void DownloadSkin() => Console.WriteLine("Downloading skin...");

        private void OpenSkinFolder()
        {
            string skinPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Skins");
            Directory.CreateDirectory(skinPath);
            Process.Start("explorer.exe", skinPath);
        }

        private void StartGame()
        {
            if (SelectedSkin == null)
            {
                Console.WriteLine("Please select a skin before starting the game.");
                return;
            }

            _window.Close();
            OnPlay?.Invoke();
        }

        public void Run()
        {
            Clock clock = new();
            while (_window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();
                _window.DispatchEvents();

                _window.Clear();
                _window.Draw(_background);
                _window.Draw(_title);

                foreach (var btn in _skinButtons) btn.UpdateDraw(_window);
                foreach (var btn in _systemButtons) btn.UpdateDraw(_window);

                _currentAnimator?.Update(deltaTime, new Vector2f(800, 300));
                _currentAnimator?.Draw(_window);

                _window.Display();
            }
        }
    }
}