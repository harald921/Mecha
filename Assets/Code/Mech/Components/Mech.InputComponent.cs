using System;

public partial class Mech // Takes input, and executes events
{
    public class InputComponent : Component
    {
        public bool isSelected { get; private set; }
        public bool isHovered  { get; private set; }

        public event System.Action OnSelected;
        public event System.Action OnSelectionLost;

        public event System.Action OnHovered;
        public event System.Action OnHoverLost;


        public InputComponent(Mech inMech) : base(inMech)
        {
            Program.inputManager.OnTileHovered += (Tile inHoveredTile) =>
            {
                if (inHoveredTile == mech.movementComponent.currentTile)
                    GainHover();

                else if (isHovered)
                    LoseHover();
            };

            Program.inputManager.OnTileClicked += (Tile inClickedTile) =>
            {
                if (inClickedTile == mech.movementComponent.currentTile)
                {
                    if (isSelected)
                        LoseSelection();
                    else
                        GainSelection();
                }

                else if (isSelected)
                    LoseSelection();
            };
        }


        void GainSelection()
        {
            isSelected = true;
            OnSelected?.Invoke();
        }

        void LoseSelection()
        {
            isSelected = false;
            OnSelectionLost?.Invoke();
        }

        void GainHover()
        {
            isHovered = true;
            OnHovered?.Invoke();
        }

        void LoseHover()
        {
            isHovered = false;
            OnHoverLost?.Invoke();
        }
    }
}
