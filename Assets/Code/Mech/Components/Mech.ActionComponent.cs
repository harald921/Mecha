public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        MoveAction _moveAction;

        public ActionComponent(Mech inMech) : base(inMech)
        {
            _moveAction = new MoveAction(mech);

            mech.OnComponentsCreated += () => mech.inputComponent.OnClicked += () =>
            {
                if (!_moveAction.isActive)
                    _moveAction.Start();
                else
                    _moveAction.Cancel();
            };
        }
    }
}
