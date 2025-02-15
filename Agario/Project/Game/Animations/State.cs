using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agario.Project.Game.Animations
{
    public class State
    {
        public string Name { get; private set; }
        public float UpdateInterval { get; set; } = 0.1f;
        public int CurrentFrame { get; protected set; }
        public int TotalFrames { get; protected set; }
        public int Row { get; private set; } 

        protected float elapsedTime;

        public State(string name, int totalFrames, int row)
        {
            Name = name;
            TotalFrames = totalFrames;
            Row = row;
        }

        public virtual void OnEnter()
        {
            CurrentFrame = 0;
            elapsedTime = 0f;
        }

        public virtual void OnExit() { }

        public virtual void Update(float deltaTime)
        {
            elapsedTime += deltaTime;

            if (elapsedTime >= UpdateInterval)
            {
                CurrentFrame = (CurrentFrame + 1) % TotalFrames;
                elapsedTime = 0f;
            }
        }
    }
}
