using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Terrain
{
    static Dictionary<TerrainType, TerrainData> _staticTerrainData = new Dictionary<TerrainType, TerrainData>()
    {
        { TerrainType.Grass, new TerrainData(inTextureID: 2, inMoveSpeedMultiplier: 1.0f, inPassable: true) },
        { TerrainType.Sand,  new TerrainData(inTextureID: 1, inMoveSpeedMultiplier: 0.5f, inPassable: true) }
    };

    readonly TerrainType _type;
    public TerrainData data => _staticTerrainData[_type];

    public Terrain(float inHeight)
    {
        if (inHeight == 0) _type = TerrainType.Grass;
        else               _type = TerrainType.Sand;
    }
}

public class TerrainData
{
    public readonly int textureID;
    public readonly float moveSpeedMultiplier;
    public readonly bool passable;


    public TerrainData(int inTextureID, float inMoveSpeedMultiplier, bool inPassable)
    {
        textureID = inTextureID;
        moveSpeedMultiplier = inMoveSpeedMultiplier;
        passable = inPassable;
    }
}

public enum TerrainType
{
    Grass,
    Sand
}