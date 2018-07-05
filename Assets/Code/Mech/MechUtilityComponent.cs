using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech
{
    public class UtilityComponent : Component
    {
        readonly Weapon[] _weaponSlots;
        readonly Utility[] _utilitySlots;


        public UtilityComponent(Mech inParentMech) : base(inParentMech)
        {
            _weaponSlots  = new Weapon[mech.bodyType.data.weaponSlotCount];
            _utilitySlots = new Utility[mech.bodyType.data.utilitySlotCount];
        }
    }
}
