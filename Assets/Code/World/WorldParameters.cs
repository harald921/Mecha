using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldParameters : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters; public NoiseParameters parameters => _parameters;

    static WorldParameters _instance;
    public static WorldParameters instance => _instance ?? (_instance = FindObjectOfType<WorldParameters>());
}
