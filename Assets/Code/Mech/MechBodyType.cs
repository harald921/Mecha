using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBodyType 
{
    Weapon[] _weaponSlots;
    Utility[] _utilitySlots;


    public MechBodyType(int inWeaponSlotCount, int inUtilitySlotCount)
    {
        _weaponSlots  = new Weapon[inWeaponSlotCount];
        _utilitySlots = new Utility[inUtilitySlotCount];
    }
}

class Weapon
{

}

class Utility
{

}

