using System.Collections.Generic;

public partial class Mech
{
    public class AttackAction : Action
    {
        readonly Mech _mechActor;

        public bool isActive { get; private set; }

        List<Tile> _tilesWithinReach;

        TilesIndicator _tilesIndicator;


        public AttackAction(Mech inMechActor, System.Action inOnCompleteCallback) : base(inOnCompleteCallback)
        {
            _mechActor = inMechActor;
        }


        public void Start()
        {
            isActive = true;

            // Calculate what tiles are eligble to be shot

            // Create indicator

            // OnTileClicked += FireIfTileIsEligble

            // OnSelectionLost += Stop;
        }

        public void Execute()
        {
            OnCompleteCallback?.Invoke();
            Stop();
        }

        public void Stop()
        {
            isActive = false;

            _tilesIndicator.Destroy();
        }
    }
}

public partial class Command
{
    public class Attack
    {

    }
}