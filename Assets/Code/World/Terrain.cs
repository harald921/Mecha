using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Terrain
{
    static Dictionary<TerrainType, TerrainData> _staticTerrainData = new Dictionary<TerrainType, TerrainData>()
    {
        { TerrainType.Grass,  new TerrainData(inTextureID: 0, inTerrainFlag: TerrainFlag.Normal)     },
        { TerrainType.Sand,   new TerrainData(inTextureID: 1, inTerrainFlag: TerrainFlag.Difficult)  },
        { TerrainType.Rock,   new TerrainData(inTextureID: 2, inTerrainFlag: TerrainFlag.Impassable) },
        { TerrainType.Water,  new TerrainData(inTextureID: 3, inTerrainFlag: TerrainFlag.Liquid)     }

    };

    readonly TerrainType _type;
    public TerrainData data => _staticTerrainData[_type];

    public Terrain(float inHeight)
    {
        if      (inHeight <= 0.25f) _type = TerrainType.Water;
        else if (inHeight <= 0.50f) _type = TerrainType.Grass;
        else if (inHeight <= 0.75f) _type = TerrainType.Sand;
        else                        _type = TerrainType.Rock;
    }
}

public class TerrainData
{
    public readonly int   textureID;
    public readonly TerrainFlag terrainFlag;

    public TerrainData(int inTextureID, TerrainFlag inTerrainFlag)
    {
        textureID   = inTextureID;
        terrainFlag = inTerrainFlag;
    }
}

public enum TerrainType
{
    Grass,
    Sand,
    Rock,
    Water
}

public enum TerrainFlag
{
    Normal,
    Difficult,
    Liquid,
    Impassable
}
