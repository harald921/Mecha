using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MechBodyType
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _weaponSlotCount;  public int weaponSlotCount  => _weaponSlotCount;
    [SerializeField] int _utilitySlotCount; public int utilitySlotCount => _utilitySlotCount; 
}

public class MechBody
{
    static MechBodyTypeDatabase _bodyTypeDatabase;
    static MechBodyTypeDatabase bodyTypeDatabase = _bodyTypeDatabase ?? (_bodyTypeDatabase = Object.FindObjectOfType<MechBodyTypeDatabase>());

    Weapon[]  _weaponSlots;
    Utility[] _utilitySlots;

    MechBodyType _bodyType;


    public MechBody(string inBodyTypeName)
    {
        _bodyType = _bodyTypeDatabase.GetBodyType(inBodyTypeName);

        _weaponSlots  = new Weapon[_bodyType.weaponSlotCount];
        _utilitySlots = new Utility[_bodyType.utilitySlotCount];
    }
}


public class Weapon
{

}

public class Utility
{

}

