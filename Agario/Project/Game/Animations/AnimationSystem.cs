using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario.Project.Game.Animations
{
    public class AnimationSystem
    {
        private AnimationFSM fsm = new AnimationFSM();
        private State idleState;
        private State walkState;
        private Sprite sprite;
        private Texture texture;
        private int frameWidth;
        private int frameHeight;

        public AnimationSystem(Texture texture, int width, int height)
        {
            this.texture = texture;
            frameWidth = width;
            frameHeight = height;

            sprite = new Sprite(this.texture);
            sprite.Origin = new Vector2f(frameWidth / 2, frameHeight / 2);

            idleState = new State("Idle", 4, 0) { UpdateInterval = 0.2f };
            walkState = new State("Walk", 18, 0) { UpdateInterval = 0.01f };

            fsm.AddState(idleState);
            fsm.AddState(walkState);

            fsm.AddTransition(idleState, walkState, () =>
                Keyboard.IsKeyPressed(Keyboard.Key.W) ||
                Keyboard.IsKeyPressed(Keyboard.Key.A) ||
                Keyboard.IsKeyPressed(Keyboard.Key.S) ||
                Keyboard.IsKeyPressed(Keyboard.Key.D));

            fsm.AddTransition(walkState, idleState, () =>
                !Keyboard.IsKeyPressed(Keyboard.Key.W) &&
                !Keyboard.IsKeyPressed(Keyboard.Key.A) &&
                !Keyboard.IsKeyPressed(Keyboard.Key.S) &&
                !Keyboard.IsKeyPressed(Keyboard.Key.D));

            fsm.SetInitialState(idleState);
        }

        public void Update(float deltaTime, Vector2f position)
        {
            fsm.Update(deltaTime);
            sprite.Position = position;

            var currentState = fsm.CurrentState;
            int frame = currentState.CurrentFrame;
            int row = currentState.Row;
            sprite.TextureRect = new IntRect(frame * frameWidth, row * frameHeight, frameWidth, frameHeight);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(sprite, states);
        }
    }
}
