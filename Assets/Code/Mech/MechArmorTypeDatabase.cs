﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechArmorTypeDatabase : MonoBehaviour
{
    static MechArmorTypeDatabase _instance;
    public static MechArmorTypeDatabase instance => _instance ?? (_instance = FindObjectOfType<MechArmorTypeDatabase>());


    [SerializeField] List<MechArmorTypeData> _armorTypesToSerialize;

    Dictionary<string, MechArmorTypeData> _armorTypes = new Dictionary<string, MechArmorTypeData>();

    
    void Awake()
    {
        foreach (MechArmorTypeData armorType in _armorTypesToSerialize)
            _armorTypes.Add(armorType.name, armorType);
    }


    public MechArmorTypeData GetArmorType(string inArmorTypeName) => _armorTypes[inArmorTypeName];
}



public class MechArmorType
{
    static MechArmorTypeDatabase _armorTypeDB = MechArmorTypeDatabase.instance;

    public MechArmorTypeData data => _armorTypeDB.GetArmorType(_armorTypeName);

    readonly string _armorTypeName;


    public MechArmorType(string inArmorTypeName)
    {
        _armorTypeName = inArmorTypeName;
    }
}



[System.Serializable]
public class MechArmorTypeData
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _movementModifier; public int movementModifier => _movementModifier;
    [SerializeField] int _armorModifier;    public int armorModifier    => _armorModifier;
}
