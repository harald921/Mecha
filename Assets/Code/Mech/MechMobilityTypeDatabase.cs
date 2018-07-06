using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMobilityTypeDatabase : MonoBehaviour
{
    static MechMobilityTypeDatabase _instance;
    public static MechMobilityTypeDatabase instance => _instance ?? (_instance = FindObjectOfType<MechMobilityTypeDatabase>());


    [SerializeField] List<MechMobilityTypeData> _mobilityTypesToSerialize;

    Dictionary<string, MechMobilityTypeData> _mobilityTypes = new Dictionary<string, MechMobilityTypeData>();
     

    void Awake()
    {
        _mobilityTypesToSerialize.ForEach(mobilityType => _mobilityTypes.Add(mobilityType.name, mobilityType));
    }


    public MechMobilityTypeData GetMobilityType(string inMobilityTypeName) => _mobilityTypes[inMobilityTypeName];
}



public class MechMobilityType
{
    static MechMobilityTypeDatabase _mobilityTypeDB = MechMobilityTypeDatabase.instance;

    public MechMobilityTypeData data => _mobilityTypeDB.GetMobilityType(_mobilityTypeName);

    readonly string _mobilityTypeName;


    public MechMobilityType(string inMobilityTypeName)
    {
        _mobilityTypeName = inMobilityTypeName;
    }
}



[System.Serializable]
public class MechMobilityTypeData
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _movementModifier; public int movementModifier => _movementModifier;
    [SerializeField] int _armorModifier;    public int armorModifier    => _armorModifier;

    [SerializeField] MobilityFlags[] _mobilityFlags;
}

public enum MobilityFlags
{
    Aerial,
    CanTravelLiquids,
    IgnoresDifficultTerrain,
    Stationary
}