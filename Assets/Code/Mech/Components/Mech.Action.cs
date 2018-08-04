public partial class Mech
{
    public abstract class Action
    {
        protected System.Action OnCompleteCallback;

        public bool isActive { get; protected set; }

        public Action(System.Action inOnCompleteCallback)
        {
            OnCompleteCallback = inOnCompleteCallback;
        }

        public abstract void Start();
        public abstract void Stop();
    }
}