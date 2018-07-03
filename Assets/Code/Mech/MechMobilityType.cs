using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMobilityType 
{
    public readonly int _movement;
    public readonly int _armor;

    MobilityFlags[] _mobilityFlags;


    public MechMobilityType(int inMovement, int inArmor, MobilityFlags[] inMobilityFlags)
    {
        _movement = inMovement;
        _armor    = inArmor;

        _mobilityFlags = inMobilityFlags;
    }
}

public enum MobilityFlags
{
    Aerial,
    CanTravelLiquids,
    IgnoresDifficultTerrain,
    Stationary
}
