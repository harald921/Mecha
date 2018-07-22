using System.Collections.Generic;
using UnityEngine;


public partial class Mech 
{
    public class MoveAction
    {
        readonly Mech _mechActor;

        TilesIndicator moveActionTilesIndicator;


        public MoveAction(Mech inMechActor)
        {
            _mechActor = inMechActor;

            List<Tile> walkableTiles = _mechActor.pathfindingComponent.FindWalkableTiles(_mechActor.movementComponent.moveSpeed);
            walkableTiles.Remove(_mechActor.movementComponent.currentTile);

            moveActionTilesIndicator = new MoveActionTilesIndicator(walkableTiles);
        }

        public void Execute() 
        {
            Debug.Log("TODO: Send this as an RPC when networking is implemented");
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


    public MoveActionTilesIndicator(List<Tile> inTiles) : base(inTiles)
    {
        _tiles = inTiles;

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
            Debug.Log("Clicked active tile: " + inTile.worldPosition);
    }
}