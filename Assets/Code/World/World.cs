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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _testMech.movementComponent.Move(Vector2DInt.Up);
        if (Input.GetKeyDown(KeyCode.S))
            _testMech.movementComponent.Move(Vector2DInt.Down);
        if (Input.GetKeyDown(KeyCode.A))
            _testMech.movementComponent.Move(Vector2DInt.Left);
        if (Input.GetKeyDown(KeyCode.D))
            _testMech.movementComponent.Move(Vector2DInt.Right);
    }


    public Tile GetTile(Vector2DInt inWorldPosition)
    {
        Vector2DInt inChunkPosition = WorldPositionToChunkPosition(inWorldPosition);
        return _chunks[inChunkPosition.x, inChunkPosition.y].GetTile(inWorldPosition);
    }

    public Chunk GetChunk(Vector2DInt inChunkPosition) => _chunks[inChunkPosition.x, inChunkPosition.y];


    public static Vector2DInt WorldPositionToChunkPosition(Vector2DInt inWorldPosition) =>
        inWorldPosition / Constants.Terrain.CHUNK_SIZE;
}




