using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters; public NoiseParameters parameters => _parameters;

    static World _instance;
    public static World instance => _instance ?? (_instance = FindObjectOfType<World>());

    static int _worldSize = Constants.Terrain.WORLD_SIZE;

    Chunk[,] _chunks = new Chunk[_worldSize, _worldSize];

    ChunkGenerator _chunkGenerator;

    Mech _testMech;

    void Awake()
    {
        _instance = this;

        _chunkGenerator = new ChunkGenerator();

        for (int y = 0; y < _worldSize; y++)
            for (int x = 0; x < _worldSize; x++)
                _chunks[x,y] = _chunkGenerator.Generate(new Vector2DInt(x, y));

        _testMech = new Mech(new MechBodyType("debug"), new MechMobilityType("debug"), new MechArmorType("debug"), _chunks[0, 0].GetTile(2, 3));
    }


    public Tile GetTile(Vector2DInt inWorldPosition)
    {
        throw new System.Exception("TODO: Implement this");
    }

    public Chunk GetChunk(Vector2DInt inChunkPosition) => _chunks[inChunkPosition.x, inChunkPosition.y];
}




