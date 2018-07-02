using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ChunkGenerator
{
    readonly ViewGenerator _viewGenerator;
    readonly DataGenerator _dataGenerator;


    public ChunkGenerator()
    {
        _viewGenerator = new ViewGenerator();
        _dataGenerator = new DataGenerator();
    }


    public Chunk Generate(Vector2DInt inPosition)
    {
        Tile[,]    tiles = _dataGenerator.Generate(inPosition);
        GameObject view  = _viewGenerator.GenerateBlank(inPosition); 

        GenerateAndSetUV2s(view, tiles);

        Chunk newChunk = new Chunk(inPosition, tiles, view); 

        newChunk.OnTilesChanged += () => GenerateAndSetUV2s(view, tiles);

        return newChunk;
    }


    void GenerateAndSetUV2s(GameObject inView, Tile[,] inTiles) =>
        inView.GetComponent<MeshFilter>().mesh.uv2 = _viewGenerator.GenerateUV2s(inTiles);
}



partial class ChunkGenerator
{
    class DataGenerator
    {
        static int _chunkSize = Constants.Terrain.CHUNK_SIZE;


        public Tile[,] Generate(Vector2DInt inChunkPosition)
        {
            float[,] noiseMap = Noise.GenerateMap(_chunkSize, inChunkPosition);

            Tile[,] newTiles = new Tile[_chunkSize, _chunkSize];
            for (int y = 0; y < _chunkSize; y++)
                for (int x = 0; x < _chunkSize; x++)
                {
                    Vector2DInt tilePosition = new Vector2DInt(x, y);

                    newTiles[x, y] = new Tile(tilePosition, inChunkPosition, new Terrain(noiseMap[x, y]));
                }

            return newTiles;
        }
    }
}



partial class ChunkGenerator
{
    class ViewGenerator
    {
        readonly int _chunkSize = Constants.Terrain.CHUNK_SIZE;

        readonly int _vertexSize;
        readonly int _vertexCount;

        readonly GameObject _templateGO;
        readonly Mesh       _templateMesh;


        public ViewGenerator()
        {
            _vertexSize = _chunkSize * 2;
            _vertexCount = _vertexSize * _vertexSize * 4;

            _templateGO = (GameObject)Resources.Load("blank_chunk", typeof(GameObject));

            _templateMesh = new Mesh()
            {
                vertices  = GenerateVertices(),
                triangles = GenerateTriangleIDs()
            };
        }


        public GameObject GenerateBlank(Vector2DInt inPosition)
        {
            GameObject newGO = Object.Instantiate(_templateGO);
            newGO.GetComponent<MeshFilter>().mesh = Object.Instantiate(_templateMesh);

            newGO.transform.position = new Vector3(inPosition.x, 0, inPosition.y) * _chunkSize;

            return newGO;
        }

        int[] GenerateTriangleIDs()
        {
            int[] triangles = new int[_chunkSize * _chunkSize * 6];
            int currentQuad = 0;
            for (int y = 0; y < _vertexSize; y += 2)
                for (int x = 0; x < _vertexSize; x += 2)
                {
                    int triangleOffset = currentQuad * 6;
                    int currentVertex = y * _vertexSize + x;

                    triangles[triangleOffset + 0] = currentVertex + 0;                 // Bottom - Left
                    triangles[triangleOffset + 1] = currentVertex + _vertexSize + 1;   // Top    - Right
                    triangles[triangleOffset + 2] = currentVertex + 1;                 // Bottom - Right

                    triangles[triangleOffset + 3] = currentVertex + 0;                 // Bottom - Left
                    triangles[triangleOffset + 4] = currentVertex + _vertexSize + 0;   // Top    - Left
                    triangles[triangleOffset + 5] = currentVertex + _vertexSize + 1;   // Top    - Right

                    currentQuad++;
                }

            return triangles;
        }

        Vector3[] GenerateVertices()
        {
            Vector3[] vertices = new Vector3[_vertexCount];

            int vertexID = 0;
            for (int y = 0; y < _chunkSize; y++)
            {
                for (int x = 0; x < _chunkSize; x++)
                {
                    // Generate a quad 
                    vertices[vertexID + 0].x = x;
                    vertices[vertexID + 0].z = y;

                    vertices[vertexID + 1].x = x + 1;
                    vertices[vertexID + 1].z = y;

                    vertices[vertexID + _vertexSize + 0].x = x;
                    vertices[vertexID + _vertexSize + 0].z = y + 1;

                    vertices[vertexID + _vertexSize + 1].x = x + 1;
                    vertices[vertexID + _vertexSize + 1].z = y + 1;

                    vertexID += 2;
                }
                vertexID += _vertexSize;
            }

            return vertices;
        }

        public Vector2[] GenerateUV2s(Tile[,] inTiles)
        {
            Vector2[] newUV2s = new Vector2[_vertexCount];

            int vertexID = 0;
            for (int y = 0; y < _chunkSize; y++)
            {
                for (int x = 0; x < _chunkSize; x++)
                {
                    int tileTextureID = inTiles[x, y].terrain.data.textureID;

                    newUV2s[vertexID + 0] = new Vector2(tileTextureID, tileTextureID);
                    newUV2s[vertexID + 1] = new Vector2(tileTextureID, tileTextureID);
                    newUV2s[vertexID + _vertexSize + 0] = new Vector2(tileTextureID, tileTextureID);
                    newUV2s[vertexID + _vertexSize + 1] = new Vector2(tileTextureID, tileTextureID);

                    vertexID += 2;
                }
                vertexID += _vertexSize;
            }

            return newUV2s;
        }
    }
}