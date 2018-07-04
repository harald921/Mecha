using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechArmorType 
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _movementModifier; public int movementModifier => _movementModifier;
    [SerializeField] int _armorModifier;    public int armorModifier    => _armorModifier;
}
