using Agario;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
public class Player
{
    public CircleShape Shape { get; private set; }
    private float _speed = 200f;
    private Vector2f _direction;

    public Player(Vector2f position)
    {
        Shape = new CircleShape(20) { FillColor = Color.Blue, Position = position };
    }

    public void HandleInput()
    {
        var direction = new Vector2f();

        if (Keyboard.IsKeyPressed(Keyboard.Key.W)) direction.Y -= 1;
        if (Keyboard.IsKeyPressed(Keyboard.Key.S)) direction.Y += 1;
        if (Keyboard.IsKeyPressed(Keyboard.Key.A)) direction.X -= 1;
        if (Keyboard.IsKeyPressed(Keyboard.Key.D)) direction.X += 1;

        _direction = Normalize(direction);
    }

    public void Update(float deltaTime)
    {
        Shape.Position += _direction * _speed * deltaTime;

        Shape.Position = new Vector2f(
            Math.Clamp(Shape.Position.X, 0, 1600 - Shape.Radius * 2),
            Math.Clamp(Shape.Position.Y, 0, 1200 - Shape.Radius * 2)
        );
    }

    public bool CheckCollision(Food food) => Shape.GetGlobalBounds().Intersects(food.Shape.GetGlobalBounds());
    public bool CheckCollision(Enemy enemy) => Shape.GetGlobalBounds().Intersects(enemy.Shape.GetGlobalBounds());

    public void Grow()
    {
        Shape.Radius += 2;
        Shape.Origin = new Vector2f(Shape.Radius, Shape.Radius);
    }

    private Vector2f Normalize(Vector2f vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        return length > 0 ? vector / length : vector;
    }

    public void Reset()
    {
        Shape.Position = new Vector2f(400, 300);
        Shape.Radius = 20; 
    }
}