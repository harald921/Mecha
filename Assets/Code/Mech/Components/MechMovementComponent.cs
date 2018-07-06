using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech
{
    public class MovementComponent : Component
    {
        public Tile currentTile { get; private set; }

        readonly int movementSpeed;

        public event Action<OnCurrentTileChangeArgs> OnCurrentTileChange;


        public MovementComponent(Mech inParentMech, Tile inSpawnTile) : base(inParentMech)
        {
            movementSpeed = mech.mobilityType.data.movementModifier + mech.armorType.data.movementModifier;

            currentTile = inSpawnTile;

            mech.OnComponentsInitialized += () => OnCurrentTileChange?.Invoke(new OnCurrentTileChangeArgs(currentTile));
        }


        public void Move(Vector2DInt inDirection)
        {
            Tile targetTile = currentTile.GetRelativeTile(inDirection);
            if (targetTile == null)
            {
                Debug.LogError("Tried to move onto null tile");
                return;
            }

            Tile previousTile = currentTile;
            currentTile = targetTile;

            OnCurrentTileChange?.Invoke(new OnCurrentTileChangeArgs(currentTile, previousTile));
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
