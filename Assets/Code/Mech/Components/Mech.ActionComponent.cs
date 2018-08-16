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

                mech.uiComponent.OnMechUIActionsDisplayed += (MechUIActions inUIActions) =>
                    inUIActions.OnWeaponButtonClicked += ToggleAttackAction;

                mech.uiComponent.OnMechUIActionsHidden += (MechUIActions inUIActions) =>
                    inUIActions.OnWeaponButtonClicked -= ToggleAttackAction;
            };

            Program.turnManager.OnNewMyTurn += () =>
                turnUsed = false;
        }

        
        void ToggleAttackAction(Weapon inWeapon)
        {
            if (turnUsed)
                return;


            if (_activeAction == null)                                                        // If there's no action...
                _activeAction = new AttackAction(mech, () => turnUsed = true, inWeapon);      // ... start an attack action
                                                                                              
            else if (_activeAction.GetType() == typeof(AttackAction))                         // If there's already an attack action...
            {                                                                                 
                _activeAction.Cancel();                                                       // ... cancel it

                if (((AttackAction)_activeAction).weaponInUse != inWeapon)                    // If the new action uses another weapon...
                    _activeAction = new AttackAction(mech, () => turnUsed = true, inWeapon);  // ... start an attack action using that weapon
                else                                                                          // If it's the same...
                    _activeAction = null;                                                     // ... null it
            }

            else
            {
                _activeAction.Cancel();
                _activeAction = new AttackAction(mech, () => turnUsed = true, inWeapon);
            }
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