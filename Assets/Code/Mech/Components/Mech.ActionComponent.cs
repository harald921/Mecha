using System;

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        public bool turnUsed { get; private set; }

        Action _activeAction;

        TilesIndicator _moveHoverTileIndicator;


        public ActionComponent(Mech inMech) : base(inMech)
        {
            mech.OnComponentsCreated += () =>
            {
                mech.inputComponent.OnSelected += ToggleMoveAction;
                
                mech.inputComponent.OnSelectionLost += () => 
                {
                    _activeAction?.Cancel();
                    _activeAction = null;
                };

                mech.inputComponent.OnHovered += () =>
                {
                    if (!turnUsed)
                        if (!mech.inputComponent.isSelected)
                            _moveHoverTileIndicator = new TilesIndicator(mech.movementComponent.GetWalkableTilePositions(), new UnityEngine.Color(0.6f, 1.0f, 0.4f, 0.3f));
                };

                mech.inputComponent.OnHoverLost += () =>
                {
                    _moveHoverTileIndicator?.Destroy();
                    _moveHoverTileIndicator = null;
                };

                mech.inputComponent.OnSelected += () =>
                {
                    _moveHoverTileIndicator.Destroy();
                    _moveHoverTileIndicator = null;
                };
            };

            Program.turnManager.OnNewMyTurn += () =>
                turnUsed = false;
        }


        void ToggleMoveAction()
        {
            if (turnUsed)
                return;

            if (_activeAction == null)
                _activeAction = new MoveAction(mech, () => turnUsed = true);

            else if (_activeAction.GetType() == typeof(MoveAction))
            {
                _activeAction.Cancel();
                _activeAction = null;
            }

            else
            {
                _activeAction.Cancel();
                _activeAction = new MoveAction(mech, () => turnUsed = true);
            }
        }
    }
}