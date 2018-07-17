using System;


public partial class Mech // Takes input, and executes events
{
    public class InputComponent : Component
    {
        public event Action OnClicked;


        public InputComponent(Mech inMech) : base(inMech) { }


        public void Click() =>
            OnClicked?.Invoke();
    }
}
