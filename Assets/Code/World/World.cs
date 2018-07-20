using System.Collections;
using UnityEngine;
using static Constants.Terrain;

public class World : MonoBehaviour
{
    [SerializeField] NoiseParameters _parameters; public NoiseParameters parameters => _parameters;

    static World _instance;
    public static World instance => _instance ?? (_instance = FindObjectOfType<World>());

    Chunk[,] _chunks = new Chunk[WORLD_SIZE_IN_CHUNKS, WORLD_SIZE_IN_CHUNKS];

    ChunkGenerator _chunkGenerator;

    public event System.Action<Tile> OnTileClicked; // TODO: Move this to some kind of WorldInputManager


    void Awake()
    {
        _instance = this;

        _chunkGenerator = new ChunkGenerator();

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
        if (Input.GetMouseButtonDown(0))
        {
            Tile clickedTile = GetTile(GetCurrentMouseWorldPos());
            if (clickedTile != null)
                OnTileClicked?.Invoke(clickedTile);
        }
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

