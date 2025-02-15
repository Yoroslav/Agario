using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agario.Project.Game.Animations
{
    public class AnimationFSM
    {
        public State CurrentState { get; private set; }
        private Dictionary<State, List<Transition>> transitions = new Dictionary<State, List<Transition>>();

        public void AddState(State state)
        {
            transitions[state] = new List<Transition>();
        }

        public void AddTransition(State from, State to, Func<bool> condition)
        {
            if (transitions.TryGetValue(from, out var existingTransitions))
            {
                existingTransitions.Add(new Transition(from, to, condition));
            }
        }

        public void SetInitialState(State initialState)
        {
            CurrentState = initialState;
            CurrentState.OnEnter();
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(deltaTime);

            if (transitions.TryGetValue(CurrentState, out var possibleTransitions))
            {
                foreach (var transition in possibleTransitions)
                {
                    if (transition.Condition())
                    {
                        SwitchState(transition.To);
                        break; 
                    }
                }
            }
        }

        private void SwitchState(State newState)
        {
            CurrentState.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}
