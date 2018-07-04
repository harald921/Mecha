using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMobilityTypeDatabase : MonoBehaviour
{
    [SerializeField] List<MechMobilityType> _mobilityTypesToSerialize;

    Dictionary<string, MechMobilityType> _mobilityTypes = new Dictionary<string, MechMobilityType>();
     

    void Awake()
    {
        foreach (MechMobilityType mobilityTypeToSerialize in _mobilityTypesToSerialize)
            _mobilityTypes.Add(mobilityTypeToSerialize.name, mobilityTypeToSerialize);
    }


    public MechMobilityType GetMobilityType(string inMobilityTypeName) => _mobilityTypes[inMobilityTypeName];

}
