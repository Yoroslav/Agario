namespace Engine
{
    public class InputController
    {
        private readonly Dictionary<string, Func<bool>> _inputActions;

        public InputController()
        {
            _inputActions = new Dictionary<string, Func<bool>>();
        }

        public void BindAction(string actionName, Func<bool> inputCondition)
        {
            _inputActions[actionName] = inputCondition;
        }

        public bool IsActionTriggered(string actionName)
        {
            return _inputActions.ContainsKey(actionName) && _inputActions[actionName]();
        }
    }
}
