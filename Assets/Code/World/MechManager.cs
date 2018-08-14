using System;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class MechManager
{
    List<Mech> _mechs = new List<Mech>();


    public void AddMech(Mech inMech) => 
        _mechs.Add(inMech);
    
    public Mech GetMech(Vector2DInt inPosition)
    {
        foreach (Mech mech in _mechs)
            if (mech.movementComponent.currentTile.worldPosition == inPosition)
                return mech;

        return null;
    }

    public Mech GetMech(Guid inGuid)
    {
        foreach (Mech mech in _mechs)
            if (mech.guid == inGuid)
                return mech;

        Debug.LogError("Mech '" + inGuid.ToString() + "' couldn't be found.");

        return null;
    }

    public void GenerateDebugMechs()
    {
        Debug.LogWarning("DEBUG: Manually spawning test-mech.");

        if (!Program.networkManager.LocalPlayer.IsMasterClient)
            return;

        new Command.SpawnMech(new Mech.Parameters()
        {
            bodyTypeName       = "debug",
            mobilityTypeName   = "debug",
            armorTypeName      = "debug",
            equipedWeaponNames = new string[] { "debugWeapon", "debugWeapon2" },
            spawnPosition      = new Vector2DInt(2, 1),
            ownerID            = Program.networkManager.LocalPlayer.ID
        }).ExecuteAndSend();

        new Command.SpawnMech(new Mech.Parameters()
        {
            bodyTypeName       = "debug",
            mobilityTypeName   = "debug",
            armorTypeName      = "debug",
            equipedWeaponNames = new string[] { "debugWeapon2" },
            spawnPosition      = new Vector2DInt(2, 3),
            ownerID            = Program.networkManager.LocalPlayer.ID
        }).ExecuteAndSend();

        new Command.SpawnMech(new Mech.Parameters()
        {
            bodyTypeName       = "debug",
            mobilityTypeName   = "debug",
            armorTypeName      = "debug",
            equipedWeaponNames = new string[] { "debugWeapon3", "debugWeapon", "debugWeapon2" },
            spawnPosition      = new Vector2DInt(2, 5),
            ownerID            = Program.networkManager.LocalPlayer.ID
        }).ExecuteAndSend();
    }
}


partial class Command
{
    public class SpawnMech : Command
    {
        public override Type type => Type.SpawnMech;

        Mech.Parameters _mechParameters;


        public SpawnMech(Mech.Parameters inParameters)
        {
            if (!Program.networkManager.LocalPlayer.IsMasterClient)
                throw new Exception("Only master client can call this command");

            _mechParameters      = inParameters;
            _mechParameters.guid = Guid.NewGuid();
        }

        public SpawnMech(NetBuffer inCommandData)
        {
            UnpackFrom(inCommandData);
        }

        public override void Execute()
        {
            Program.mechManager.AddMech(new Mech(_mechParameters));
        }


        public override int GetPacketSize() =>
            _mechParameters.GetPacketSize();

        public override void PackInto(NetBuffer inBuffer) =>
            _mechParameters.PackInto(inBuffer);

        public override void UnpackFrom(NetBuffer inBuffer) =>
            _mechParameters.UnpackFrom(inBuffer);
   
    }
}