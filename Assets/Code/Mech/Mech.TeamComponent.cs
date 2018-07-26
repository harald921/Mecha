using System.Collections.Generic;
using UnityEngine;

public partial class Mech
{
    public class TeamComponent : Component
    {
        static List<Color> _teamColors = new List<Color>()
        {
            Color.blue,
            Color.red,
            Color.green,
            Color.yellow, 
        };

        public Color teamColor => _teamColors[ID];

        public readonly int ID;

        public TeamComponent(Mech inParentMech, int inID) : base(inParentMech)
        {
            ID = inID;
        }
    }
}