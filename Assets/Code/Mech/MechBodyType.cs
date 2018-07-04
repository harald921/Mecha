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


    Weapon  GetWeapon(int inSlotID)  => _weaponSlots[inSlotID];
    Utility GetUtility(int inSlotID) => _utilitySlots[inSlotID];
}

class Weapon
{

}

class Utility
{

}

