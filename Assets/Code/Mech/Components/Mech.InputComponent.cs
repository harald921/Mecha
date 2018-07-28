using System;


public partial class Mech // Takes input, and executes events
{
    public class InputComponent : Component
    {
        public event System.Action OnClicked;


        public InputComponent(Mech inMech) : base(inMech)
        {
            Program.inputManager.OnTileClicked += (Tile inClickedTile) =>
            {
                if (inClickedTile == mech.movementComponent.currentTile)
                    OnClicked?.Invoke();
            };
        }
    }
}
