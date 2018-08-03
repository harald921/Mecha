using System;

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        public bool turnUsed { get; private set; }

        MoveAction _moveAction;


        public ActionComponent(Mech inMech) : base(inMech)
        {
            _moveAction = new MoveAction(mech, () => turnUsed = true);

            mech.OnComponentsCreated += () =>
            {
                mech.inputComponent.OnSelected += () => 
                    mech.uiComponent.mechGUI.OnMoveButtonPressed += ToggleMoveAction;
                mech.inputComponent.OnSelectionLost += () =>
                    mech.uiComponent.mechGUI.OnMoveButtonPressed -= ToggleMoveAction;
            };

            Program.turnManager.OnNewMyTurn += () =>
                turnUsed = false;
        }

        void ToggleMoveAction()
        {
            if (turnUsed)
                return;

            if (!_moveAction.isActive)
                _moveAction.Start();
            else
                _moveAction.Stop();
        }

    }
}