using System;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;
using System.Linq;

public partial class Mech 
{
    public class MoveAction : Action
    {
        Vector2DInt[] _walkableTilePositions;

        TilesIndicator _tilesIndicator;


        public MoveAction(Mech inMechActor, System.Action inOnCompleteCallback) : base(inMechActor, inOnCompleteCallback)
        {
            _walkableTilePositions = _mechActor.movementComponent.GetWalkableTilePositions();

            _tilesIndicator = new TilesIndicator(_walkableTilePositions, new UnityEngine.Color(0, 1, 0, 0.5f));

            Program.inputManager.OnTileClicked += ExecuteIfTileIsWalkable;
            _mechActor.inputComponent.OnSelectionLost += Cancel;
        }

        public override void Cancel()
        {
            _tilesIndicator.Destroy();

            Program.inputManager.OnTileClicked -= ExecuteIfTileIsWalkable;
            _mechActor.inputComponent.OnSelectionLost -= Cancel;
        }


        void Execute(Tile inTargetTile) 
        {
            new Command.MoveMech(_mechActor, inTargetTile.worldPosition).ExecuteAndSend();

            OnCompleteCallback?.Invoke();
            Cancel();
        }

        void ExecuteIfTileIsWalkable(Tile inTile)
        {
            if (_walkableTilePositions.Contains(inTile.worldPosition))
                Execute(inTile);
        }
    }
}


partial class Command
{
    public class MoveMech : Command
    {
        public override Type type => Type.MoveMech;

        Guid        _targetMechGuid;
        Vector2DInt _destination;


        public MoveMech(Mech inTargetMech, Vector2DInt inDestination)
        {
            _targetMechGuid = inTargetMech.guid;
            _destination = inDestination;
        }

        public MoveMech(NetBuffer inCommandData)
        {
            UnpackFrom(inCommandData);
        }

        public override void Execute()
        {
            Program.mechManager.GetMech(_targetMechGuid).movementComponent.TryMoveTo(Program.world.GetTile(_destination));
        }


        public override int GetPacketSize() =>
            NetUtility.BitsToHoldGuid(_targetMechGuid) +
            _destination.GetPacketSize();

        public override void PackInto(NetBuffer inBuffer)
        {
            _targetMechGuid.PackInto(inBuffer);
            _destination.PackInto(inBuffer);
        }

        public override void UnpackFrom(NetBuffer inBuffer)
        {
            _targetMechGuid.UnpackFrom(inBuffer, ref _targetMechGuid);
            _destination.UnpackFrom(inBuffer);
        }
    }
}