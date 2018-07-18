using System.Collections.Generic;
using UnityEngine;


public partial class Mech // TODO: Clean up
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

            _walkableTilesView = new WalkableTilesView(GetWalkablePositions());
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


        List<Vector2DInt> GetWalkablePositions()
        {
            int moveSpeed = _mechActor.movementComponent.moveSpeed;
            Vector2DInt currentPosition = _mechActor.movementComponent.currentTile.worldPosition;

            // Calculate possible positions
            List<Vector2DInt> possiblePositions = new List<Vector2DInt>();
            for (int y = -moveSpeed; y <= moveSpeed; y++)
                for (int x = -moveSpeed; x <= moveSpeed; x++)
                    possiblePositions.Add(new Vector2DInt(x, y) + currentPosition);

            // Test which positions are possible to reach within this turn
            List<Vector2DInt> walkablePositions = new List<Vector2DInt>();
            foreach (Vector2DInt possiblePosition in possiblePositions)
            {
                Tile possibleTile = World.instance.GetTile(possiblePosition);
                if (possibleTile != null)
                {
                    int distanceToPossibleTile = _mechActor.pathfindingComponent.FindPath(possibleTile).distance;

                    if (distanceToPossibleTile <= moveSpeed * 10)
                        if (distanceToPossibleTile > 0)
                            walkablePositions.Add(possiblePosition); 
                }
            }

            return walkablePositions;
        }
    }
}
