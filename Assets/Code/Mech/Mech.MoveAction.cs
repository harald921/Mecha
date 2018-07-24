using System;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech 
{
    public class MoveAction : Action
    {
        readonly Mech _mechActor;

        TilesIndicator moveActionTilesIndicator;


        public MoveAction(Mech inMechActor, System.Action inOnCompleteCallback) : base(inOnCompleteCallback)
        {
            _mechActor = inMechActor;

            List<Tile> walkableTiles = _mechActor.pathfindingComponent.FindWalkableTiles(_mechActor.movementComponent.moveSpeed);
            walkableTiles.Remove(_mechActor.movementComponent.currentTile);

            moveActionTilesIndicator = new MoveActionTilesIndicator(walkableTiles, Execute);
        }

        public void Execute(Tile inTargetTile) 
        {
            Debug.Log("TODO: Send this as an RPC when networking is implemented");
            _mechActor.movementComponent.TryMoveTo(inTargetTile);

            OnCompleteCallback?.Invoke();
            Cancel();
        }

        public void Cancel()
        {
            moveActionTilesIndicator.Destroy();
            moveActionTilesIndicator = null;
        }
    }
}


public class MoveActionTilesIndicator : TilesIndicator
{
    List<Tile> _tiles;

    Action<Tile> _onTileClickedCallback;


    public MoveActionTilesIndicator(List<Tile> inTiles, Action<Tile> inOnTileClickedCallback) : base(inTiles)
    {
        _tiles = inTiles;

        _onTileClickedCallback = inOnTileClickedCallback;

        World.instance.inputManager.OnTileClicked += ExecuteIfContainsTile;
    }

    public override void Destroy()
    {
        base.Destroy();

        World.instance.inputManager.OnTileClicked -= ExecuteIfContainsTile;
    }


    void ExecuteIfContainsTile(Tile inTile)
    {
        if (_tiles.Contains(inTile))
            _onTileClickedCallback?.Invoke(inTile);
    }
}