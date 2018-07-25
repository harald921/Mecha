using System;

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        public bool onCooldown { get; private set; }

        MoveAction _moveAction;


        public ActionComponent(Mech inMech) : base(inMech)
        {
            mech.OnComponentsCreated += () => mech.inputComponent.OnClicked += () =>
            {
                if (onCooldown)
                    return;

                if (_moveAction == null)
                    _moveAction = new MoveAction(inMech, () =>
                    {
                        onCooldown = true;
                        _moveAction = null;
                    });

                else
                {
                    _moveAction.Cancel();
                    _moveAction = null;
                }
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