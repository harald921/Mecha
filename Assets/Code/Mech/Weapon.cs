using Lidgren.Network;
using UnityEngine;

public class Weapon : IPackable
{
    static WeaponDatabase _weaponDB = WeaponDatabase.instance;

    public WeaponData data => _weaponDB.GetWeaponData(_name);

    string _name;


    public Weapon(string inWeaponName)
    {
        _name = inWeaponName;
    }


    public int GetPacketSize() =>
        NetUtility.BitsToHoldString(_name);

    public void PackInto(NetBuffer inBuffer) =>
        inBuffer.Write(_name);

    public void UnpackFrom(NetBuffer inBuffer) =>
        _name = inBuffer.ReadString();
}


[System.Serializable]
public class WeaponData
{
    [SerializeField] string _name; public string name => _name;

    [SerializeField] int _range;  public int range  => _range;
    [SerializeField] int _damage; public int damage => _damage;
}

