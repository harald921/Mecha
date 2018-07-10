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

        OnComponentsCreated?.Invoke();
        OnComponentsInitialized?.Invoke();
    }
}


