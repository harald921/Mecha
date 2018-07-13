using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech
{
    public readonly MechBodyType     bodyType;
    public readonly MechMobilityType mobilityType;
    public readonly MechArmorType    armorType;

    public readonly UtilityComponent     utilityComponent;
    public readonly MovementComponent    movementComponent;
    public readonly PathfindingComponent pathfindingComponent;
    public readonly ViewComponent        viewComponent;
    public readonly InputComponent       inputComponent;
    public readonly ActionComponent      actionComponent;
    public readonly UIComponent          uiComponent;

    public event Action OnComponentsCreated;
    public event Action OnComponentsInitialized;


    public Mech(MechBodyType inBodyType, MechMobilityType inMobilityType, MechArmorType inArmorType, Tile inSpawnTile)
    {
        bodyType     = inBodyType;
        mobilityType = inMobilityType;
        armorType    = inArmorType;

        utilityComponent     = new UtilityComponent(this);
        movementComponent    = new MovementComponent(this, inSpawnTile);
        pathfindingComponent = new PathfindingComponent(this);
        viewComponent        = new ViewComponent(this);
        inputComponent       = new InputComponent(this);
        actionComponent      = new ActionComponent(this);
        uiComponent          = new UIComponent(this);

        OnComponentsCreated?.Invoke();
        OnComponentsInitialized?.Invoke();
    }
}


public partial class Mech // Takes input, and executes events
{
    public class InputComponent : Component
    {
        public event Action OnClicked;


        public InputComponent(Mech inMech) : base(inMech) { }


        public void Click() =>
            OnClicked?.Invoke();
    }
}

public partial class Mech // Decides what action to "begin" depending on the input
{
    public class ActionComponent : Component
    {
        MoveAction _moveAction;

        public ActionComponent(Mech inMech) : base(inMech)
        {
            _moveAction = new MoveAction(mech);

            mech.OnComponentsCreated += () => mech.inputComponent.OnClicked += () =>
            {
                if (!_moveAction.isActive)
                    _moveAction.Start();
                else
                    _moveAction.Cancel();
            };
        }

    }
}


public partial class Mech // Holds references to UI, and displays it depending on what is happening
{
    public class UIComponent : Component
    {
        public UIComponent(Mech inMech) : base(inMech)
        {

        }
    }
}


public partial class Mech
{
    public class MoveAction
    {
        public bool isActive { get; private set; }

        readonly Mech _mechActor;


        public MoveAction(Mech inMechActor)
        {
            _mechActor = inMechActor;
        }

        public void Start()
        {
            isActive = true;

            Debug.Log("move action started (display walkable tiles)");
        }

        public void Execute() // This should be the RPC
        {

        }

        public void Cancel()
        {
            Debug.Log("move action canceled (hide walkable tiles and UI)");
        }
    }
}