using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech
{
    public class UtilityComponent : Component
    {
        readonly List<Weapon> _weapons = new List<Weapon>();


        public UtilityComponent(Mech inParentMech, string[] inWeaponNames) : base(inParentMech)
        {
            foreach (string weaponName in inWeaponNames)
                _weapons.Add(new Weapon(weaponName));
        }


        public Weapon GetWeapon(int inSlotID)   => _weapons[inSlotID];
    }
}
