using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MechMobilityType 
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _movement;
    [SerializeField] int _armor;

    [SerializeField] MobilityFlags[] _mobilityFlags;
}

public enum MobilityFlags
{
    Aerial,
    CanTravelLiquids,
    IgnoresDifficultTerrain,
    Stationary
}

    
