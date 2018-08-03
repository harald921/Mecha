using UnityEngine;

public class Weapon
{
    static WeaponDatabase _weaponDB = WeaponDatabase.instance;

    public WeaponData data => _weaponDB.GetWeaponData(_name);

    readonly string _name;


    public Weapon(string inWeaponName)
    {
        _name = inWeaponName;
    }
}


[System.Serializable]
public class WeaponData
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _range;  public int range  => _range;
    [SerializeField] int _damage; public int damage => _damage;
}

