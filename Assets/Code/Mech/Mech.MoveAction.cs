using System;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech 
{
    public class MoveAction : Action
    {
        readonly Mech _mechActor;

        public bool isActive { get; private set; }

        List<Tile> _walkableTiles;

        TilesIndicator tilesIndicator;


        public MoveAction(Mech inMechActor, System.Action inOnCompleteCallback) : base(inOnCompleteCallback)
        {
            _mechActor = inMechActor;

        }

        public void Start()
        {
            isActive = true;

            _walkableTiles = _mechActor.pathfindingComponent.FindWalkableTiles(_mechActor.movementComponent.moveSpeed);
            _walkableTiles.Remove(_mechActor.movementComponent.currentTile);

            tilesIndicator = new TilesIndicator(_walkableTiles);

            Program.inputManager.OnTileClicked += ExecuteIfTileIsWalkable;
        }

        public void Execute(Tile inTargetTile) 
        {
            Debug.Log("TODO: Send this as an RPC when networking is implemented");
            _mechActor.movementComponent.TryMoveTo(inTargetTile);

            OnCompleteCallback?.Invoke();
            Stop();
        }

        public void Stop()
        {
            isActive = false;

            tilesIndicator.Destroy();

            Program.inputManager.OnTileClicked -= ExecuteIfTileIsWalkable;
        }


        void ExecuteIfTileIsWalkable(Tile inTile)
        {
            if (_walkableTiles.Contains(inTile))
                Execute(inTile);
        }
    }
}
