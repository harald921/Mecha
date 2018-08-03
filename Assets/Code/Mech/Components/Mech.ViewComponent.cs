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
            Color.cyan,
        };

        public readonly GameObject _viewGO;
        public readonly MeshRenderer _viewMeshRenderer;

        public ViewComponent(Mech inParentMech) : base(inParentMech)
        {
            _viewGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _viewMeshRenderer = _viewGO.GetComponent<MeshRenderer>();

            _viewMeshRenderer.material.color = _teamColors[mech.owner.ID];

            mech.OnComponentsCreated += () => 
                mech.movementComponent.OnCurrentTileChange += UpdatePosition;

            mech.OnComponentsCreated += () =>
                mech.inputComponent.OnSelected += () => 
                    _viewMeshRenderer.material.color = Color.yellow;

            mech.OnComponentsCreated += () =>
                mech.inputComponent.OnSelectionLost += () => 
                    _viewMeshRenderer.material.color = _teamColors[mech.owner.ID];

        }


        void UpdatePosition(OnCurrentTileChangeArgs inTileChangeArgs)
        {
            Vector2DInt newPosition = inTileChangeArgs.currentTile.worldPosition;

            _viewGO.transform.position = new Vector3(newPosition.x + 0.5f, 0.5f, newPosition.y + 0.5f);
        }
    }
}