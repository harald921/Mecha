using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech 
{
    public readonly MechBodyType     bodyType;
    public readonly MechMobilityType mobilityType;
    public readonly MechArmorType    armorType;


    public Mech(MechBodyType inBodyType, MechMobilityType inMobilitytype, MechArmorType inArmorType)
    {
        bodyType     = inBodyType;
        mobilityType = inMobilitytype;
        armorType    = inArmorType;
    }
}
