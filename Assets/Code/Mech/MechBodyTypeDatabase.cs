using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBodyTypeDatabase : MonoBehaviour
{
    static MechBodyTypeDatabase _instance;
    public static MechBodyTypeDatabase instance => _instance ?? (_instance = FindObjectOfType<MechBodyTypeDatabase>());


    [SerializeField] List<MechBodyTypeData> _bodyTypesToSerialize;

    Dictionary<string, MechBodyTypeData> _bodyTypes = new Dictionary<string, MechBodyTypeData>();


    void Awake()
    {
        foreach (MechBodyTypeData bodyType in _bodyTypesToSerialize)
            _bodyTypes.Add(bodyType.name, bodyType);
    }


    public MechBodyTypeData GetBodyType(string inBodyTypeName) => _bodyTypes[inBodyTypeName];
}



public class MechBodyType
{
    static MechBodyTypeDatabase _bodyTypeDatabase = MechBodyTypeDatabase.instance;

    public MechBodyTypeData data => _bodyTypeDatabase.GetBodyType(_bodyTypeName);

    readonly string _bodyTypeName;


    public MechBodyType(string inBodyTypeName)
    {
        _bodyTypeName = inBodyTypeName;
    }
}



[System.Serializable]
public class MechBodyTypeData
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _weaponSlots;  public int weaponSlotCount  => _weaponSlots;
    [SerializeField] int _utilitySlots; public int utilitySlotCount => _utilitySlots;
}