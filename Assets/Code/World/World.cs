using System.Collections;
using UnityEngine;

using static Constants.Terrain;

public partial class World
{
    Chunk[,] _chunks = new Chunk[WORLD_SIZE_IN_CHUNKS, WORLD_SIZE_IN_CHUNKS];

    ChunkGenerator _chunkGenerator;


    public World()
    {
        _chunkGenerator = new ChunkGenerator();

        GenerateChunks();
    }


    public Chunk GetChunk(Vector2DInt inChunkPosition) => 
        _chunks[inChunkPosition.x, inChunkPosition.y];

    public Tile GetTile(Vector2DInt inWorldPosition)
    {
        if (inWorldPosition.x < 0 || inWorldPosition.y < 0)
            return null;

        if (inWorldPosition.x >= WORLD_SIZE_IN_TILES || inWorldPosition.y >= WORLD_SIZE_IN_TILES)
            return null;

        Vector2DInt chunkPosition = WorldPosToChunkPos(inWorldPosition);
        Vector2DInt tilePosition  = WorldPosToLocalTilePos(inWorldPosition);

        return _chunks[chunkPosition.x, chunkPosition.y].GetTile(tilePosition);
    }

    void GenerateChunks()
    {
        for (int y = 0; y < WORLD_SIZE_IN_CHUNKS; y++)
            for (int x = 0; x < WORLD_SIZE_IN_CHUNKS; x++)
                _chunks[x, y] = _chunkGenerator.Generate(new Vector2DInt(x, y));
    }


    public static Vector2DInt WorldPosToChunkPos(Vector2DInt inWorldPosition) =>
        inWorldPosition / CHUNK_SIZE;

    public static Vector2DInt WorldPosToLocalTilePos(Vector2DInt inWorldPosition) =>
        inWorldPosition % CHUNK_SIZE;
}
