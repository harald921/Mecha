using System;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public partial class Mech 
{
    public class MoveAction : Action
    {
        readonly Mech _mechActor;

        List<Tile> _walkableTiles;

        TilesIndicator _tilesIndicator;


        public MoveAction(Mech inMechActor, System.Action inOnCompleteCallback) : base(inOnCompleteCallback)
        {
            _mechActor = inMechActor;
        }

        public override void Start()
        {
            isActive = true;

            _walkableTiles = _mechActor.pathfindingComponent.FindWalkableTiles(_mechActor.movementComponent.moveSpeed);
            _walkableTiles.Remove(_mechActor.movementComponent.currentTile);

            _tilesIndicator = new TilesIndicator(_walkableTiles);

            Program.inputManager.OnTileClicked += ExecuteIfTileIsWalkable;

            _mechActor.inputComponent.OnSelectionLost += Stop;
        }

        public void Execute(Tile inTargetTile) 
        {
            new Command.MoveMech(_mechActor, inTargetTile.worldPosition).ExecuteAndSend();

            OnCompleteCallback?.Invoke();
            Stop();
        }

        public override void Stop()
        {
            isActive = false;

            _tilesIndicator.Destroy();

            Program.inputManager.OnTileClicked -= ExecuteIfTileIsWalkable;

            _mechActor.inputComponent.OnSelectionLost -= Stop;
        }


        void ExecuteIfTileIsWalkable(Tile inTile)
        {
            if (_walkableTiles.Contains(inTile))
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