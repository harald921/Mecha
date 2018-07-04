using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public readonly Vector2DInt position;
    public readonly Vector2DInt chunkPosition;

    public Terrain terrain { get; private set; }


    public Tile(Vector2DInt inPosition, Vector2DInt inChunkPosition, Terrain inTerrain)
    {
        position = inPosition;
        chunkPosition = inChunkPosition;

        terrain = inTerrain;
    }
}