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

        public event EventHandler<OnCurrentTileChangeArgs> OnCurrentTileChange;


        public MovementComponent(Mech inParentMech, Tile inSpawnTile) : base(inParentMech)
        {
            movementSpeed = mech.mobilityType.data.movementModifier + mech.armorType.data.movementModifier;

            currentTile = inSpawnTile;


            mech.OnComponentsInitialized += () => OnCurrentTileChange?.Invoke(this, new OnCurrentTileChangeArgs(currentTile));
        }


        public void Move(Vector2DInt inDirection)
        {
            throw new Exception("TODO: Implement this");
        }
    }
}

public class OnCurrentTileChangeArgs : EventArgs
{
    public Tile currentTile;
    public Tile previousTile;


    public OnCurrentTileChangeArgs(Tile inCurrentTile, Tile inPreviusTile = null)
    {
        currentTile  = inCurrentTile;
        previousTile = inPreviusTile;
    }
}