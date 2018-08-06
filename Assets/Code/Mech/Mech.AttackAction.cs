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

            _tilesIndicator = new TilesIndicator(_positionsWithinRange);

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
            List<Vector2DInt> tilesWithinRange = new List<Vector2DInt>();
            int weaponRange = _mechActor.utilityComponent.GetWeapon(0).data.range;
            for (int y = 0; y < weaponRange; y++)
                for (int x = 0; x < weaponRange; x++)
                {
                    Vector2DInt currentPosition = new Vector2DInt(x, y);
                    if (currentPosition.magnitude <= weaponRange)
                        tilesWithinRange.Add(currentPosition);
                }

            return tilesWithinRange;
        }
    }
}

public partial class Command
{
    public class Attack
    {

    }
}