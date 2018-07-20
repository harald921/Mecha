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
                    _moveAction = new MoveAction(inMech);

                else
                {
                    _moveAction.Cancel();
                    _moveAction = null;
                }
            };
        }
    }
}
