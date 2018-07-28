using System;

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        public bool turnUsed { get; private set; }

        MoveAction _moveAction;


        public ActionComponent(Mech inMech) : base(inMech)
        {
            _moveAction = new MoveAction(mech, () => _moveAction = null);

            mech.OnComponentsCreated += () => mech.inputComponent.OnClicked += () =>
            {
                if (turnUsed)
                    return;

                if (!_moveAction.isActive)
                    _moveAction.Start();
                else
                    _moveAction.Stop();
            };
        }
    }
}


public partial class Mech
{
    public class Action
    {
        protected System.Action OnCompleteCallback;

        public Action(System.Action inOnCompleteCallback)
        {
            OnCompleteCallback = inOnCompleteCallback;
        }
    }
}