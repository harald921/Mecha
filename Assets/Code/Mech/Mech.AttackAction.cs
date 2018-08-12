using System.Collections.Generic;
using System.Linq;

public partial class Mech
{
    public class AttackAction : Action
    {
        Weapon _weaponInUse;

        Vector2DInt[] _positionsWithinRange;

        TilesIndicator _tilesIndicator;



        public AttackAction(Mech inMechActor, System.Action inOnCompleteCallback, Weapon inWeaponToUse) : base(inMechActor, inOnCompleteCallback)
        {
            UnityEngine.Debug.LogError("TODO: Make it possible to choose which weapon to fire");

            _weaponInUse = inWeaponToUse;

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
            inTargetTile.mech?.healthComponent.ModifyHealth(-_weaponInUse.data.damage);

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
            List<Vector2DInt> positionsWithinRange = new List<Vector2DInt>();
            int               weaponRange          = _weaponInUse.data.range;
            Vector2DInt       currentPosition      = _mechActor.movementComponent.currentTile.worldPosition;

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