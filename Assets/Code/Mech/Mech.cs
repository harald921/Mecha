using System;
using System.Collections;
using Lidgren.Network;
using ExitGames.Client.Photon.LoadBalancing;

public partial class Mech
{
    public readonly Guid   guid;
    public readonly Player owner;

    public readonly MechBodyType     bodyType;
    public readonly MechMobilityType mobilityType;
    public readonly MechArmorType    armorType;

    public readonly HealthComponent      healthComponent;
    public readonly UtilityComponent     utilityComponent;
    public readonly MovementComponent    movementComponent;
    public readonly PathfindingComponent pathfindingComponent;
    public readonly ViewComponent        viewComponent;
    public readonly InputComponent       inputComponent;
    public readonly ActionComponent      actionComponent;
    public readonly UIComponent          uiComponent;

    public event System.Action OnComponentsCreated;
    public event System.Action OnComponentsInitialized;


    public Mech(Parameters inParameters)
    {
        guid  = inParameters.guid;
        owner = Program.networkManager.CurrentRoom.GetPlayer(inParameters.ownerID);    

        bodyType     = new MechBodyType(inParameters.bodyTypeName);
        mobilityType = new MechMobilityType(inParameters.mobilityTypeName);
        armorType    = new MechArmorType(inParameters.armorTypeName);

        healthComponent      = new HealthComponent(this);
        utilityComponent     = new UtilityComponent(this, inParameters.equipedWeaponNames);
        movementComponent    = new MovementComponent(this, inParameters.spawnPosition);
        pathfindingComponent = new PathfindingComponent(this);
        viewComponent        = new ViewComponent(this);
        inputComponent       = new InputComponent(this);
        actionComponent      = new ActionComponent(this);
        uiComponent          = new UIComponent(this);

        OnComponentsCreated?.Invoke();
        OnComponentsInitialized?.Invoke();
    }


    public struct Parameters : IPackable
    {
        public string bodyTypeName;
        public string mobilityTypeName;
        public string armorTypeName;

        public string[] equipedWeaponNames;

        public Vector2DInt spawnPosition;

        public int  ownerID;
        public Guid guid;


        public int GetPacketSize()
        {
            int numBits = 0;

            numBits += NetUtility.BitsToHoldString(bodyTypeName);
            numBits += NetUtility.BitsToHoldString(mobilityTypeName);
            numBits += NetUtility.BitsToHoldString(armorTypeName);
            numBits += NetUtility.BitsToHoldUInt((uint)equipedWeaponNames.Length);

            foreach (string equipedWeaponName in equipedWeaponNames)
                numBits += NetUtility.BitsToHoldString(equipedWeaponName);

            numBits += spawnPosition.GetPacketSize();
            numBits += NetUtility.BitsToHoldUInt((uint)ownerID);
            numBits += NetUtility.BitsToHoldGuid(guid);

            return numBits;
        }

        public void PackInto(NetBuffer inBuffer)
        {
            inBuffer.Write(bodyTypeName);
            inBuffer.Write(mobilityTypeName);
            inBuffer.Write(armorTypeName);

            inBuffer.WriteVariableInt32(equipedWeaponNames.Length);
            foreach (string equipedWeaponName in equipedWeaponNames)
                inBuffer.Write(equipedWeaponName);

            spawnPosition.PackInto(inBuffer);

            inBuffer.WriteVariableInt32(ownerID);
            guid.PackInto(inBuffer);
        }

        public void UnpackFrom(NetBuffer inBuffer)
        {
            bodyTypeName     = inBuffer.ReadString();
            mobilityTypeName = inBuffer.ReadString();
            armorTypeName    = inBuffer.ReadString();

            int equipedWeaponNamesCount = inBuffer.ReadVariableInt32();
            equipedWeaponNames = new string[equipedWeaponNamesCount];
            for (int i = 0; i < equipedWeaponNamesCount; i++)
                equipedWeaponNames[i] = inBuffer.ReadString();

            spawnPosition.UnpackFrom(inBuffer);

            ownerID = inBuffer.ReadVariableInt32();
            guid.UnpackFrom(inBuffer, ref guid);
        }
    }
}
