using System.Collections.Generic;
using UnityEngine;

public class MechManager
{
    List<Mech> _mechs = new List<Mech>();

    public Mech GetMech(Vector2DInt inPosition)
    {
        foreach (Mech mech in _mechs)
            if (mech.movementComponent.currentTile.worldPosition == inPosition)
                return mech;

        return null;
    }


    public void GenerateDebugMechs()
    {
        Debug.LogWarning("DEBUG: Manually spawning test-mech.");

        _mechs.Add(new Mech(new MechBodyType("debug"), new MechMobilityType("debug"), new MechArmorType("debug"), World.instance.GetTile(2, 1), 0));
        _mechs.Add(new Mech(new MechBodyType("debug"), new MechMobilityType("debug"), new MechArmorType("debug"), World.instance.GetTile(2, 3), 0));
        _mechs.Add(new Mech(new MechBodyType("debug"), new MechMobilityType("debug"), new MechArmorType("debug"), World.instance.GetTile(2, 5), 0));
    }
}