using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    static WeaponDatabase _instance;
    public static WeaponDatabase instance => _instance ?? (_instance = FindObjectOfType<WeaponDatabase>());

    [SerializeField] List<WeaponData> _weaponsToSerialize;

    Dictionary<string, WeaponData> _weapons = new Dictionary<string, WeaponData>();

    bool _initialized;


    void Awake() => Initialize();

    void Initialize()
    {
        if (_initialized)
            return;

        _weaponsToSerialize.ForEach(weaponToSerialize => _weapons.Add("weaponName", weaponToSerialize));
        _initialized = true;
    }

    public WeaponData GetWeaponData(string inWeaponName)
    {
        Initialize();

        return _weapons[inWeaponName];
    }
}
