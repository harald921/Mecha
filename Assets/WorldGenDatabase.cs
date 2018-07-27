using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenDatabase : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters; public NoiseParameters parameters => _parameters;

    static WorldGenDatabase _instance;
    public static WorldGenDatabase instance => _instance ?? (_instance = FindObjectOfType<WorldGenDatabase>());
}
