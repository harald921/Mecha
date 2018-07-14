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

        WalkableTilesView _walkableTilesView;


        public MoveAction(Mech inMechActor)
        {
            _mechActor = inMechActor;
        }

        public void Start()
        {
            isActive = true;

            Debug.Log("TODO: Calculate what tiles can be moved to");
            
            { // Debug: Create a 3x3 grid of walkable tiles around the mech 
                List<Vector2DInt> walkableTilePositions = new List<Vector2DInt>(); // _mechActor.movementComponent.GetTilesWithinMoveRange();
                for (int y = -2; y < 3; y++)
                    for (int x = -2; x < 3; x++)
                        walkableTilePositions.Add(new Vector2DInt(x, y) + _mechActor.movementComponent.currentTile.worldPosition);

                _walkableTilesView = new WalkableTilesView(walkableTilePositions);
            }

            Debug.Log("move action started (display walkable tiles)");
        }

        public void Execute() 
        {
            Debug.Log("TODO: Send this as an RPC when networking is implemented");
        }

        public void Cancel()
        {
            isActive = false;

            _walkableTilesView.Destroy();
            _walkableTilesView = null;
            Debug.Log("move action canceled (hide walkable tiles and UI)");
        }
    }
}

public class WalkableTilesView
{
    GameObject[] _views;

    public WalkableTilesView(List<Vector2DInt> inPositions)
    {
        _views = new GameObject[inPositions.Count];

        for (int i = 0; i < inPositions.Count; i++)
        {
            GameObject tileView = GameObject.Instantiate(Resources.Load<GameObject>("Prefab_WalkableTile"));

            tileView.transform.position = new Vector3(inPositions[i].x + 0.5f, 1, inPositions[i].y + 0.5f);

            _views[i] = tileView;
        }
    }

    public void Destroy()
    {
        foreach (GameObject view in _views)
            GameObject.Destroy(view);
    }
}