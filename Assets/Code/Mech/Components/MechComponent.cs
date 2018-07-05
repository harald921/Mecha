using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Mech
{
    public class Component
    {
        readonly protected Mech mech;


        public Component(Mech inParentMech)
        {
            mech = inParentMech;
        }
    }
}




