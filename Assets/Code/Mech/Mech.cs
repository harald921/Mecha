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


public partial class Mech // TODO: Clean up
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

            _walkableTilesView = new WalkableTilesView(GetWalkablePositions());
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
        }


        List<Vector2DInt> GetWalkablePositions()
        {
            int moveSpeed = _mechActor.movementComponent.moveSpeed;
            Vector2DInt currentPosition = _mechActor.movementComponent.currentTile.worldPosition;

            // Calculate possible positions
            List<Vector2DInt> possiblePositions = new List<Vector2DInt>();
            for (int y = -moveSpeed; y <= moveSpeed; y++)
                for (int x = -moveSpeed; x <= moveSpeed; x++)
                    possiblePositions.Add(new Vector2DInt(x, y) + currentPosition);

            // Test which positions are possible to reach within this turn
            List<Vector2DInt> walkablePositions = new List<Vector2DInt>();
            foreach (Vector2DInt possiblePosition in possiblePositions)
            {
                Tile possibleTile = World.instance.GetTile(possiblePosition);
                if (possibleTile != null)
                {
                    int distanceToPossibleTile = _mechActor.pathfindingComponent.FindPath(possibleTile).distance;

                    if (distanceToPossibleTile <= moveSpeed * 10)
                        if (distanceToPossibleTile > 0)
                            walkablePositions.Add(possiblePosition); 
                }
            }

            return walkablePositions;
        }
    }
}

public class WalkableTilesView // TODO: Clean up
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