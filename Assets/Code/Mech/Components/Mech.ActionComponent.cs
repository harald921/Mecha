using System;

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        public bool turnUsed { get; private set; }

        MoveAction _moveAction;
        AttackAction _attackAction;


        public ActionComponent(Mech inMech) : base(inMech)
        {
            _moveAction   = new MoveAction(mech, () => turnUsed = true);
            _attackAction = new AttackAction(mech, () => turnUsed = true);

            mech.OnComponentsCreated += () =>
            {
                System.Action tryToggleMoveAction   = () => TryToggleAction(_moveAction);
                System.Action tryToggleAttackAction = () => TryToggleAction(_attackAction);

                mech.inputComponent.OnSelected += () =>
                {
                    mech.uiComponent.mechGUI.OnMoveButtonPressed   += tryToggleMoveAction;
                    mech.uiComponent.mechGUI.OnAttackButtonPressed += tryToggleAttackAction;
                };

                mech.inputComponent.OnSelectionLost += () =>
                {
                    mech.uiComponent.mechGUI.OnMoveButtonPressed   -= tryToggleMoveAction;
                    mech.uiComponent.mechGUI.OnAttackButtonPressed -= tryToggleAttackAction;
                };
            };

            Program.turnManager.OnNewMyTurn += () =>
                turnUsed = false;
        }

        void TryToggleAction(Action inAction)
        {
            if (turnUsed)
                return;

            if (!inAction.isActive)
                inAction.Start();
            else
                inAction.Stop();
        }
    }
}