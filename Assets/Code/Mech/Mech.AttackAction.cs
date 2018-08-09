using System.Collections.Generic;
using System.Linq;

public partial class Mech
{
    public class AttackAction : Action
    {
        Vector2DInt[] _positionsWithinRange;

        TilesIndicator _tilesIndicator;


        protected override void OnStart()
        {
            _positionsWithinRange = GetPositionsWithinRange();

            _tilesIndicator = new TilesIndicator(_positionsWithinRange, new UnityEngine.Color(1, 0, 0, 0.5f));

            Program.inputManager.OnTileClicked += ExecuteIfShootable;
        }

        public override void Cancel()
        {
            _tilesIndicator.Destroy();

            Program.inputManager.OnTileClicked -= ExecuteIfShootable;
        }


        void Execute(Tile inTargetTile)
        {
            OnCompleteCallback?.Invoke();
            Cancel();
        }

        void ExecuteIfShootable(Tile inTile)
        {
            if (_positionsWithinRange.Contains(inTile.worldPosition))
                Execute(inTile);
        }

        Vector2DInt[] GetPositionsWithinRange()
        {
            UnityEngine.Debug.LogError("TODO: Finish implement GetPositionsWithinRange()");

            Vector2DInt currentPosition = _mechActor.movementComponent.currentTile.worldPosition;

            List<Vector2DInt> positionsWithinRange = new List<Vector2DInt>();
            int weaponRange = 4; // _mechActor.utilityComponent.GetWeapon(0).data.range;
            for (int y = -weaponRange; y <= weaponRange; y++)
                for (int x = -weaponRange; x <= weaponRange; x++)
                {
                    Vector2DInt positionOffset = new Vector2DInt(x, y);

                    if (positionOffset.magnitude <= weaponRange)
                        if (Program.world.GetTile(positionOffset + currentPosition) != null)
                            positionsWithinRange.Add(positionOffset + currentPosition);
                }

            return positionsWithinRange.ToArray();
        }
    }
}

public partial class Command
{
    public class Attack
    {

    }
}