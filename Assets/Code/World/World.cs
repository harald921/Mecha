using System.Collections;
using UnityEngine;


public class World : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters; public NoiseParameters parameters => _parameters;

    static World _instance;
    public static World instance => _instance ?? (_instance = FindObjectOfType<World>());

    static int _worldSize = Constants.Terrain.WORLD_SIZE_IN_CHUNKS;

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

    }

    void Start()
    {
        _testMech = new Mech(new MechBodyType("debug"), new MechMobilityType("debug"), new MechArmorType("debug"), _chunks[0, 0].GetTile(2, 3));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (GetCurrentMouseWorldPos() == _testMech.movementComponent.currentTile.worldPosition)
                _testMech.inputComponent.Click();
            else
                _testMech.movementComponent.TryMoveTo(GetTile(GetCurrentMouseWorldPos()));
    }


    public Tile GetTile(Vector2DInt inWorldPosition)
    {
        if (inWorldPosition.x < 0 || inWorldPosition.y < 0)
            return null;

        if (inWorldPosition.x >= Constants.Terrain.WORLD_SIZE_IN_TILES || inWorldPosition.y >= Constants.Terrain.WORLD_SIZE_IN_TILES)
            return null;

        Vector2DInt chunkPosition = WorldPosToChunkPos(inWorldPosition);
        Vector2DInt tilePosition  = WorldPosToLocalTilePos(inWorldPosition);

        return _chunks[chunkPosition.x, chunkPosition.y].GetTile(tilePosition);
    }

    public Chunk GetChunk(Vector2DInt inChunkPosition) => 
        _chunks[inChunkPosition.x, inChunkPosition.y];


    public static Vector2DInt WorldPosToChunkPos(Vector2DInt inWorldPosition) =>
        inWorldPosition / Constants.Terrain.CHUNK_SIZE;

    public static Vector2DInt WorldPosToLocalTilePos(Vector2DInt inWorldPosition) =>
        inWorldPosition % Constants.Terrain.CHUNK_SIZE;

    public static Vector2DInt GetCurrentMouseWorldPos()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2DInt((int)mouseWorldPosition.x, (int)mouseWorldPosition.z);
    }
}

