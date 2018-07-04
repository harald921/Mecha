using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBodyTypeDatabase : MonoBehaviour
{
    [SerializeField] List<MechBodyType> _bodyTypesToSerialize;

    Dictionary<string, MechBodyType> _bodyTypes = new Dictionary<string, MechBodyType>();


    void Awake()
    {
        foreach (MechBodyType bodyType in _bodyTypesToSerialize)
            _bodyTypes.Add(bodyType.name, bodyType);
    }


    public MechBodyType GetBodyType(string inBodyTypeName) => _bodyTypes[inBodyTypeName];
}
