using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public readonly Vector2DInt worldPosition;
    public readonly Vector2DInt chunkPosition;

    public Terrain terrain { get; private set; }

    public readonly Node node;


    public Tile(Vector2DInt inPosition, Vector2DInt inChunkPosition, Terrain inTerrain)
    {
        node = new Node(this);

        worldPosition = inPosition;
        chunkPosition = inChunkPosition;

        terrain = inTerrain;
    }

    public List<Tile> GetNeighbours()
    {
        List<Tile> neighbours = new List<Tile>();

        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Up));
        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Left));
        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Right));
        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Down));
        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Up + Vector2DInt.Left));
        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Up + Vector2DInt.Right));
        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Down + Vector2DInt.Left));
        neighbours.AddIfNotNull(GetRelativeTile(Vector2DInt.Down + Vector2DInt.Right));

        return neighbours;
    }

    public Tile GetRelativeTile(Vector2DInt inOffset) =>
        World.instance.GetTile(worldPosition + inOffset);
}