using System.Collections;
using UnityEngine;
using static Constants.Terrain;

public partial class World : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters; public NoiseParameters parameters => _parameters;

    static World _instance;
    public static World instance => _instance ?? (_instance = FindObjectOfType<World>());

    Chunk[,] _chunks = new Chunk[WORLD_SIZE_IN_CHUNKS, WORLD_SIZE_IN_CHUNKS];

    public WorldInputManager inputManager { get; private set; }

    ChunkGenerator    _chunkGenerator;


    void Awake()
    {
        _instance = this;

        _chunkGenerator = new ChunkGenerator();

        inputManager = new WorldInputManager(this);

        for (int y = 0; y < WORLD_SIZE_IN_CHUNKS; y++)
            for (int x = 0; x < WORLD_SIZE_IN_CHUNKS; x++)
                _chunks[x,y] = _chunkGenerator.Generate(new Vector2DInt(x, y));
    }

    void Start()
    {
        new Mech(new MechBodyType("debug"), new MechMobilityType("debug"), new MechArmorType("debug"), _chunks[0, 0].GetTile(2, 3));
    }

    void Update()
    {
        inputManager.ManualUpdate();
    }


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

    public Chunk GetChunk(Vector2DInt inChunkPosition) => 
        _chunks[inChunkPosition.x, inChunkPosition.y];


    public static Vector2DInt WorldPosToChunkPos(Vector2DInt inWorldPosition) =>
        inWorldPosition / CHUNK_SIZE;

    public static Vector2DInt WorldPosToLocalTilePos(Vector2DInt inWorldPosition) =>
        inWorldPosition % CHUNK_SIZE;
}
