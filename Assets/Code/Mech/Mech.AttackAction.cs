using System;
using System.Collections.Generic;
using System.Linq;
using Lidgren.Network;

public partial class Mech
{
    public class AttackAction : Action
    {
        public readonly Weapon weaponInUse;

        Vector2DInt[] _positionsWithinRange;

        TilesIndicator _tilesIndicator;



        public AttackAction(Mech inMechActor, System.Action inOnCompleteCallback, Weapon inWeaponInUse) : base(inMechActor, inOnCompleteCallback)
        {
            weaponInUse = inWeaponInUse;

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
            inTargetTile.mech?.healthComponent.ModifyHealth(-weaponInUse.data.damage);

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
            int               weaponRange          = weaponInUse.data.range;
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
    public class Attack : Command
    {
        public override Type type => Type.Attack;

        Guid        _sourceMechGuid;
        Weapon      _usedWeapon;
        Vector2DInt _targetPosition;


        public Attack(Vector2DInt inTargetPosition)
        {
            _targetPosition = inTargetPosition;
        }

        public Attack(NetBuffer inCommandData)
        {
            UnpackFrom(inCommandData);
        }


        public override void Execute()
        {
            UnityEngine.Debug.Log("TODO: Make a Shoot() function for the Weapon class");
            Program.world.GetTile(_targetPosition).mech?.healthComponent.ModifyHealth(-_usedWeapon.data.damage);
        }


        public override int GetPacketSize() =>
            NetUtility.BitsToHoldGuid(_sourceMechGuid) +
            _usedWeapon.GetPacketSize()                +
            _targetPosition.GetPacketSize();

        public override void PackInto(NetBuffer inBuffer)
        {
            _sourceMechGuid.PackInto(inBuffer);
            _usedWeapon.PackInto(inBuffer);
            _targetPosition.PackInto(inBuffer);
        }

        public override void UnpackFrom(NetBuffer inBuffer)
        {
            _sourceMechGuid.UnpackFrom(inBuffer, ref _sourceMechGuid);
            _usedWeapon.UnpackFrom(inBuffer);
            _targetPosition.UnpackFrom(inBuffer);
        }
    }
}