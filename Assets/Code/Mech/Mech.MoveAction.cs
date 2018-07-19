using System.Collections.Generic;
using UnityEngine;


public partial class Mech 
{
    public class MoveAction
    {
        public bool isActive { get; private set; }

        readonly Mech _mechActor;

        WalkableTilesView _walkableTilesView;


        public MoveAction(Mech inMechActor)
        {
            _mechActor = inMechActor;
        }

        public void Start()
        {
            isActive = true;

            _walkableTilesView = new WalkableTilesView(_mechActor.pathfindingComponent.FindWalkableTiles(_mechActor.movementComponent.moveSpeed));
        }

        public void Execute() 
        {
            Debug.Log("TODO: Send this as an RPC when networking is implemented");
        }

        public void Cancel()
        {
            isActive = false;

            _walkableTilesView.Destroy();
            _walkableTilesView = null;
        }
    }
}
