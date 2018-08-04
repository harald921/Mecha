using System.Collections.Generic;

public partial class Mech
{
    public class AttackAction : Action
    {
        readonly Mech _mechActor;

        List<Tile> _tilesWithinRange;

        TilesIndicator _tilesIndicator;


        public AttackAction(Mech inMechActor, System.Action inOnCompleteCallback) : base(inOnCompleteCallback)
        {
            _mechActor = inMechActor;
        }


        public override void Start()
        {
            isActive = true;

            _tilesWithinRange = GetTilesWithinRange();

            _tilesIndicator = new TilesIndicator(_tilesWithinRange);

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
            if (_tilesWithinRange.Contains(inTile))
                Execute(inTile);
        }

        List<Tile> GetTilesWithinRange()
        {
            List<Tile> tilesWithinRange = new List<Tile>();
            int weaponRange = _mechActor.utilityComponent.GetWeapon(0).data.range;
            for (int y = 0; y < weaponRange; y++)
                for (int x = 0; x < weaponRange; x++)
                {
                    Vector2DInt currentTilePos = new Vector2DInt(x, y);
                    if (currentTilePos.magnitude <= weaponRange)
                        tilesWithinRange.Add(Program.world.GetTile(currentTilePos));
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