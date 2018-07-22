using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Constants.Terrain;

public partial class World : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters; public NoiseParameters parameters => _parameters;

    static World _instance;
    public static World instance => _instance ?? (_instance = FindObjectOfType<World>());

    Chunk[,] _chunks = new Chunk[WORLD_SIZE_IN_CHUNKS, WORLD_SIZE_IN_CHUNKS];

    public WorldInputManager inputManager { get; private set; }
    public WorldMechManager  mechManager  { get; private set; }

    ChunkGenerator _chunkGenerator;


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
        mechManager = new WorldMechManager();
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

    public Chunk GetChunk(int inChunkPositionX, int inChunkPositionY) =>
        GetChunk(new Vector2DInt(inChunkPositionX, inChunkPositionY));

    public static Vector2DInt WorldPosToChunkPos(Vector2DInt inWorldPosition) =>
        inWorldPosition / CHUNK_SIZE;

    public static Vector2DInt WorldPosToLocalTilePos(Vector2DInt inWorldPosition) =>
        inWorldPosition % CHUNK_SIZE;
}


public class WorldMechManager
{
    List<Mech> _mechs = new List<Mech>();

    public Mech GetMech(Vector2DInt inPosition)
    {
        foreach (Mech mech in _mechs)
            if (mech.movementComponent.currentTile.worldPosition == inPosition)
                return mech;

        return null;
    }


    public WorldMechManager()
    {
        Debug.LogWarning("DEBUG: Manually spawning test-mech. WorldMechManager ctor should be moved to Awake ASAP");

        Tile debugTargetTile = World.instance.GetChunk(0, 0).GetTile(2, 3); 
        _mechs.Add(new Mech(new MechBodyType("debug"), new MechMobilityType("debug"), new MechArmorType("debug"), debugTargetTile)); 
    }
}