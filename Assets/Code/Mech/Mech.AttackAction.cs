using System.Collections.Generic;

public partial class Mech
{
    public class AttackAction : Action
    {
        readonly Mech _mechActor;

        List<Vector2DInt> _positionsWithinRange;

        TilesIndicator _tilesIndicator;


        public AttackAction(Mech inMechActor, System.Action inOnCompleteCallback) : base(inOnCompleteCallback)
        {
            _mechActor = inMechActor;
        }


        public override void Start()
        {
            isActive = true;

            _positionsWithinRange = GetPositionsWithinRange();

            _tilesIndicator = new TilesIndicator(_positionsWithinRange, new UnityEngine.Color(1, 0, 0, 0.5f));

            Program.inputManager.OnTileClicked += FireAtTileIfWithinRange;
            _mechActor.inputComponent.OnSelectionLost += Stop;
        }

        public void Execute(Tile inTargetTile)
        {
            OnCompleteCallback?.Invoke();
            Stop();
        }

        public override void Stop()
        {
            isActive = false;

            _tilesIndicator.Destroy();

            Program.inputManager.OnTileClicked -= FireAtTileIfWithinRange;
            _mechActor.inputComponent.OnSelectionLost -= Stop;
        }

        void FireAtTileIfWithinRange(Tile inTile)
        {
            if (_positionsWithinRange.Contains(inTile.worldPosition))
                Execute(inTile);
        }

        List<Vector2DInt> GetPositionsWithinRange()
        {
            UnityEngine.Debug.LogError("TODO: Finish implement GetPositionsWithinRange()");

            Vector2DInt currentPosition = _mechActor.movementComponent.currentTile.worldPosition;

            List<Vector2DInt> positionsWithinRange = new List<Vector2DInt>();
            int weaponRange = 4; // _mechActor.utilityComponent.GetWeapon(0).data.range;
            for (int y = -weaponRange; y <= weaponRange; y++)
                for (int x = -weaponRange; x <= weaponRange; x++)
                {
                    Vector2DInt positionOffset = new Vector2DInt(x, y);
                    UnityEngine.Debug.Log(positionOffset);
                    if (positionOffset.magnitude <= weaponRange)
                        positionsWithinRange.Add(positionOffset + currentPosition);
                }

            return positionsWithinRange;
        }
    }
}

public partial class Command
{
    public class Attack
    {

    }
}