using Agario.Project.Game.MenuSkins;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
    public event Action OnPlay; // Событие для запуска игры

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
        _buttonTexture = ResourceManager.GetUITexture("button_base");
        _font = ResourceManager.GetFont();
    }

    private void SetupUI()
    {
        _background = new RectangleShape(new Vector2f(_window.Size.X, _window.Size.Y))
        {
            FillColor = new Color(40, 40, 40)
        };

        _title = new Text("Skin Selector", _font, 36)
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
        Vector2f startPos = new(50, 100);
        float buttonSpacing = 70;

        foreach (var name in skinNames)
        {
            var button = new UIButton(
                _buttonTexture,
                _font,
                name,
                new Vector2f(startPos.X, startPos.Y + _skinButtons.Count * buttonSpacing),
                () => SelectSkin(name)
            );
            _skinButtons.Add(button);
        }
    }

    private void SetupSystemButtons()
    {
        Vector2f startPos = new(50, 500);
        float spacing = 150;

        _systemButtons.Add(new UIButton(_buttonTexture, _font, "Download Skin", startPos, DownloadSkin));
        _systemButtons.Add(new UIButton(_buttonTexture, _font, "Open Folder", startPos + new Vector2f(spacing, 0), OpenSkinFolder));
        _systemButtons.Add(new UIButton(_buttonTexture, _font, "Exit", startPos + new Vector2f(spacing * 2, 0), () => _window.Close()));
        _systemButtons.Add(new UIButton(_buttonTexture, _font, "Play", startPos + new Vector2f(spacing * 3, 0), () => OnPlay?.Invoke()));
    }

    private void SelectSkin(string skinName)
    {
        SelectedSkin = ResourceManager.GetSkinTexture(skinName);

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
    }

    private void DownloadSkin() => Console.WriteLine("Downloading skin...");

    private void OpenSkinFolder()
    {
        string skinPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Skins");
        Directory.CreateDirectory(skinPath);
        Process.Start("explorer.exe", skinPath);
    }

    public void Run()
    {
        Clock clock = new();
        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            float deltaTime = clock.Restart().AsSeconds();

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