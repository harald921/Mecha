using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech
{
    public class ViewComponent : Component
    {
        public readonly GameObject _viewGO;


        public ViewComponent(Mech inParentMech) : base(inParentMech)
        {
            _viewGO = GameObject.CreatePrimitive(PrimitiveType.Cube);

            mech.OnComponentsCreated += () => mech.movementComponent.OnCurrentTileChange += UpdatePosition;
        }


        void UpdatePosition(OnCurrentTileChangeArgs inTileChangeArgs)
        {
            Vector2DInt newPosition = inTileChangeArgs.currentTile.worldPosition;

            _viewGO.transform.position = new Vector3(newPosition.x + 0.5f, 0.5f, newPosition.y + 0.5f);
        }
    }
}