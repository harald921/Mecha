using System;
using System.Collections;
using Lidgren.Network;

public partial class Mech
{
    public readonly Guid guid;

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


    public Mech(Parameters inParameters)
    {
        guid = (Guid)inParameters.guid;
            
        bodyType     = new MechBodyType(inParameters.bodyTypeName);
        mobilityType = new MechMobilityType(inParameters.mobilityTypeName);
        armorType    = new MechArmorType(inParameters.armorTypeName);

        utilityComponent     = new UtilityComponent(this);
        movementComponent    = new MovementComponent(this, inParameters.spawnPosition);
        pathfindingComponent = new PathfindingComponent(this);
        viewComponent        = new ViewComponent(this);
        inputComponent       = new InputComponent(this);
        actionComponent      = new ActionComponent(this);
        uiComponent          = new UIComponent(this);
        teamComponent        = new TeamComponent(this, inParameters.team);

        OnComponentsCreated?.Invoke();
        OnComponentsInitialized?.Invoke();
    }


    public struct Parameters : IPackable
    {
        public string bodyTypeName;
        public string mobilityTypeName;
        public string armorTypeName;

        public Vector2DInt spawnPosition;

        public int         team;
        public Guid        guid;


        public int GetPacketSize()
        {
            return NetUtility.BitsToHoldString(bodyTypeName)     +
                   NetUtility.BitsToHoldString(mobilityTypeName) +
                   NetUtility.BitsToHoldString(armorTypeName)    +
                   
                   spawnPosition.GetPacketSize()                 +
            
                   NetUtility.BitsToHoldUInt((uint)team)         +
                   NetUtility.BitsToHoldGuid(guid);
        }

        public void PackInto(NetBuffer inBuffer)
        {
            inBuffer.Write(bodyTypeName);
            inBuffer.Write(mobilityTypeName);
            inBuffer.Write(armorTypeName);

            spawnPosition.PackInto(inBuffer);

            inBuffer.WriteVariableInt32(team);
            guid.PackInto(inBuffer);
        }

        public void UnpackFrom(NetBuffer inBuffer)
        {
            bodyTypeName     = inBuffer.ReadString();
            mobilityTypeName = inBuffer.ReadString();
            armorTypeName    = inBuffer.ReadString();

            spawnPosition.UnpackFrom(inBuffer);

            team = inBuffer.ReadVariableInt32();
            guid.UnpackFrom(inBuffer, ref guid);
        }
    }
}


