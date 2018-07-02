using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters;

    public static float[,] GenerateMap(int inSize, Vector2DInt inOffset)
    {
        float[,] noiseMap = new float[inSize, inSize];

        for (int y = 0; y < inSize; y++)
            for (int x = 0; x < inSize; x++)
                noiseMap[x, y] = Random.Range(0, 2);

        return noiseMap;
    }
}

[System.Serializable]
public struct NoiseParameters
{
    public int   scale;
    public int   octaves;
    public float persistance;
    public float lacunarity;

    public int   seed;
}
