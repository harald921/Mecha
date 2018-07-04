using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechArmorTypeDatabase : MonoBehaviour
{
    [SerializeField] List<MechArmorType> _armorTypesToSerialize;

    Dictionary<string, MechArmorType> _armorTypes = new Dictionary<string, MechArmorType>();

    
    void Awake()
    {
        foreach (MechArmorType armorType in _armorTypesToSerialize)
            _armorTypes.Add(armorType.name, armorType);
    }


    public MechArmorType GetArmorType(string inArmorTypeName) => _armorTypes[inArmorTypeName];
}
