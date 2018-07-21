using System.Collections.Generic;
using UnityEngine;


public partial class Mech 
{
    public class MoveAction
    {
        readonly Mech _mechActor;

        TilesIndicator _walkableTilesView;


        public MoveAction(Mech inMechActor)
        {
            _mechActor = inMechActor;
            _walkableTilesView = new MoveActionTilesIndicator(_mechActor.pathfindingComponent.FindWalkableTiles(_mechActor.movementComponent.moveSpeed));
        }

        public void Execute() 
        {
            Debug.Log("TODO: Send this as an RPC when networking is implemented");
        }

        public void Cancel()
        {
            _walkableTilesView.Destroy();
            _walkableTilesView = null;
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