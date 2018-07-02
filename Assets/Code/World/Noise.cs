using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public static float[,] GenerateMap(int inSize, Vector2DInt inOffset, NoiseParameters inParameters)
    {
        float[,] noiseMap = new float[inSize, inSize];

        for (int y = 0; y < inSize; y++)
            for (int x = 0; x < inSize; x++)
            {
                float sampleX = (x + (inOffset.x * inSize)) * inParameters.frequency;
                float sampleY = (y + (inOffset.y * inSize)) * inParameters.frequency;

                for (int i = 0; i < inParameters.octaves; i++)
                {
                    float octaveModifier = Mathf.Pow(2, i);
                    noiseMap[x, y] += (1 / octaveModifier) * Mathf.PerlinNoise(sampleX * octaveModifier, sampleY * octaveModifier);
                }

                noiseMap[x, y] -= 0.5f;
            }

        return noiseMap;
    }
}

[System.Serializable]
public struct NoiseParameters
{
    public float frequency;
    public int   octaves;
    public float persistance;
    public float lacunarity;

    public int   seed;
}
