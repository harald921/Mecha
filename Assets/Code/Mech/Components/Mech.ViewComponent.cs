using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech
{
    public class ViewComponent : Component
    {
        static List<Color> _teamColors = new List<Color>()
        {
            Color.magenta,

            Color.blue,
            Color.red,
            Color.green,
            Color.yellow,
        };

        public readonly GameObject _viewGO;


        public ViewComponent(Mech inParentMech) : base(inParentMech)
        {
            _viewGO = GameObject.CreatePrimitive(PrimitiveType.Cube);

            mech.OnComponentsCreated += () => mech.movementComponent.OnCurrentTileChange += UpdatePosition;

            mech.OnComponentsInitialized += () => _viewGO.GetComponent<MeshRenderer>().material.color = _teamColors[mech.owner.ID];
        }


        void UpdatePosition(OnCurrentTileChangeArgs inTileChangeArgs)
        {
            Vector2DInt newPosition = inTileChangeArgs.currentTile.worldPosition;

            _viewGO.transform.position = new Vector3(newPosition.x + 0.5f, 0.5f, newPosition.y + 0.5f);
        }
    }
}