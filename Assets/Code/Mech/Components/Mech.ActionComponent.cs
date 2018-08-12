using System;

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        public bool turnUsed { get; private set; }

        Action _activeAction;


        public ActionComponent(Mech inMech) : base(inMech)
        {
            mech.OnComponentsCreated += () =>
            {
                mech.inputComponent.OnSelected += () => 
                    mech.uiComponent.mechGUI.OnMoveButtonPressed += ToggleMoveAction;
                
                mech.inputComponent.OnSelectionLost += () => 
                {
                    _activeAction?.Cancel();
                    _activeAction = null;

                    mech.uiComponent.mechGUI.OnMoveButtonPressed -= ToggleMoveAction;
                };
            };

            Program.turnManager.OnNewMyTurn += () =>
                turnUsed = false;
        }

        void OnActionCompleted()
        {
            turnUsed = true;
            _activeAction = null;
        }

        void ToggleMoveAction()
        {
            if (_activeAction == null)
                _activeAction = new MoveAction(mech, () => turnUsed = true);

            else if (_activeAction.GetType() == typeof(MoveAction))
            {
                _activeAction.Cancel();
                _activeAction = null;
            }

            else
            {
                _activeAction.Cancel();
                _activeAction = new MoveAction(mech, () => turnUsed = true);
            }
        }
    }
}