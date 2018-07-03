using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMobilityTypeDatabase : MonoBehaviour
{
    [SerializeField] List<MechMobilityType> _mobilityTypesToSerialize;

    public Dictionary<string, MechMobilityType> _mobilityType { get; private set; } = new Dictionary<string, MechMobilityType>();


    void Awake()
    {
        foreach (MechMobilityType mobilityTypeToSerialize in _mobilityTypesToSerialize)
        {
            _mobilityType.Add(mobilityTypeToSerialize.name, mobilityTypeToSerialize);
        }
    }
}
