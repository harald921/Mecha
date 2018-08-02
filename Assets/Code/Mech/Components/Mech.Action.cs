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