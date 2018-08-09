public partial class Mech
{
    public abstract class Action
    {
        protected Mech _mechActor { get; private set; }

        protected System.Action OnCompleteCallback;


        public void Initialize(Mech inMechActor, System.Action inOnCompleteCallback)
        {
            _mechActor = inMechActor;
            OnCompleteCallback = inOnCompleteCallback;

            OnStart();
        }

        public abstract void Cancel();
        protected abstract void OnStart();
    }
}