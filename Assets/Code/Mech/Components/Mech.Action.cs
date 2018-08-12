public partial class Mech
{
    public abstract class Action
    {
        protected Mech _mechActor { get; private set; }

        protected System.Action OnCompleteCallback;


        public Action(Mech inMechActor, System.Action inOnCompleteCallback)
        {
            _mechActor = inMechActor;
            OnCompleteCallback = inOnCompleteCallback;
        }

        public abstract void Cancel();
    }
}