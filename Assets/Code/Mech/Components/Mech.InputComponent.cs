using System;

public partial class Mech // Takes input, and executes events
{
    public class InputComponent : Component
    {
        bool _isSelected;

        public event System.Action OnSelected;
        public event System.Action OnSelectionLost;

        public InputComponent(Mech inMech) : base(inMech)
        {
            Program.inputManager.OnTileClicked += (Tile inClickedTile) =>
            {
                if (inClickedTile == mech.movementComponent.currentTile)
                {
                    if (_isSelected)
                        LostSelection();
                    else
                        GainSelection();
                }

                else if (_isSelected)
                    LostSelection();
            };
        }


        void GainSelection()
        {
            _isSelected = true;
            OnSelected?.Invoke();
        }

        void LostSelection()
        {
            _isSelected = false;
            OnSelectionLost?.Invoke();
        }
    }
}
