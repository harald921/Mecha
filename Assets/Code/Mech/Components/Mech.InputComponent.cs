using System;

public partial class Mech // Takes input, and executes events
{
    public class InputComponent : Component
    {
        bool _selected;

        public event System.Action OnSelected;
        public event System.Action OnSelectionLost;

        public InputComponent(Mech inMech) : base(inMech)
        {
            Program.inputManager.OnTileClicked += (Tile inClickedTile) =>
            {
                if (inClickedTile == mech.movementComponent.currentTile)
                {
                    _selected = true;
                    OnSelected?.Invoke();
                }

                else
                {
                    if (_selected)
                    {
                        _selected = false;
                        OnSelectionLost?.Invoke();
                    }
                }
            };
        }
    }
}
