using System;
using System.Collections;


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
    public readonly TeamComponent        teamComponent;

    public event System.Action OnComponentsCreated;
    public event System.Action OnComponentsInitialized;


    public Mech(MechBodyType inBodyType, MechMobilityType inMobilityType, MechArmorType inArmorType, Tile inSpawnTile, int inTeam)
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
        teamComponent        = new TeamComponent(this, inTeam);

        OnComponentsCreated?.Invoke();
        OnComponentsInitialized?.Invoke();
    }
}

