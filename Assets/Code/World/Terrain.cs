using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Terrain
{
    static Dictionary<TerrainType, TerrainData> _staticTerrainData = new Dictionary<TerrainType, TerrainData>()
    {
        { TerrainType.Grass,  new TerrainData(inTextureID: 0, inMoveSpeedMultiplier: 1.0f, inPassable: true) },
        { TerrainType.Sand,   new TerrainData(inTextureID: 1, inMoveSpeedMultiplier: 0.5f, inPassable: true) },
        { TerrainType.Rock,   new TerrainData(inTextureID: 2, inMoveSpeedMultiplier: 0.5f, inPassable: true) },
        { TerrainType.Water,  new TerrainData(inTextureID: 3, inMoveSpeedMultiplier: 0.5f, inPassable: true) }

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
    Sand,
    Rock,
    Water
}