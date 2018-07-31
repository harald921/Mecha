using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public partial class Mech
{
    public class MovementComponent : Component
    {
        public Tile currentTile { get; private set; }

        public readonly int moveSpeed;

        public event Action<OnCurrentTileChangeArgs> OnCurrentTileChange;


        public MovementComponent(Mech inParentMech, Vector2DInt inSpawnPosition) : base(inParentMech)
        {
            moveSpeed = mech.mobilityType.data.movementModifier + mech.armorType.data.movementModifier;

            currentTile = Program.world.GetTile(inSpawnPosition);

            mech.OnComponentsInitialized += () => OnCurrentTileChange?.Invoke(new OnCurrentTileChangeArgs(currentTile));
        }


        public void TryMoveTo(Tile inDestination)
        {
            List<Tile> pathTiles = mech.pathfindingComponent.FindPath(inDestination).tiles;

            if (pathTiles.Count > 0)
                Timing.RunCoroutineSingleton(_DoMoveAnimation(pathTiles), 0, SingletonBehavior.Overwrite);       

            else
                Debug.LogError("Couldn't move to destination");
        }

        
        IEnumerator<float> _DoMoveAnimation(List<Tile> inPath)
        {
            foreach (Tile tile in inPath)
            {
                Tile previousTile = currentTile;
                currentTile = tile;

                OnCurrentTileChange(new OnCurrentTileChangeArgs(currentTile, previousTile));

                yield return Timing.WaitForSeconds(0.1f);
            }
        }
    }
}



public class OnCurrentTileChangeArgs
{
    public Tile currentTile;
    public Tile previousTile;


    public OnCurrentTileChangeArgs(Tile inCurrentTile, Tile inPreviusTile = null)
    {
        currentTile  = inCurrentTile;
        previousTile = inPreviusTile;
    }
}
