using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechArmorType 
{
    public readonly int _movementModifier;
    public readonly int _armorModifier;


    public MechArmorType(int inMovementModifier, int inArmorModifier)
    {
        _movementModifier = inMovementModifier;
        _armorModifier = inArmorModifier;
    }
}
