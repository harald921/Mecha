using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour
{
    static int _worldSize = Constants.Terrain.WORLD_SIZE;
    Chunk[,]   _chunks    = new Chunk[_worldSize, _worldSize];

    ChunkGenerator _chunkGenerator;

    void Awake()
    {
        _chunkGenerator = new ChunkGenerator();

        for (int y = 0; y < _worldSize; y++)
            for (int x = 0; x < _worldSize; x++)
                _chunkGenerator.Generate(new Vector2DInt(x, y));
    }
}



