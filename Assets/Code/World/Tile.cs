using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    readonly Vector2DInt _position;
    readonly Vector2DInt _chunkPosition;

    public Terrain terrain { get; private set; }


    public Tile(Vector2DInt inPosition, Vector2DInt inChunkPosition, Terrain inTerrain)
    {
        _position = inPosition;
        _chunkPosition = inChunkPosition;

        terrain = inTerrain;
    }
}