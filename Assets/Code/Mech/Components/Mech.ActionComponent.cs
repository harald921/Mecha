using System;

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        MoveAction _moveAction;


        public ActionComponent(Mech inMech) : base(inMech)
        {
            mech.OnComponentsCreated += () => mech.inputComponent.OnClicked += () =>
            {
                if (_moveAction == null)
                    _moveAction = new MoveAction(inMech, () => _moveAction = null);

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