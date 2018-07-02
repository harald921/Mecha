using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public readonly Vector2DInt position;
    public readonly GameObject  viewGO;

    readonly Tile[,] _tiles;


    public event Action OnTilesChanged;


    public Chunk(Vector2DInt inPosition, Tile[,] inTiles, GameObject inViewGO)
    {
        position = inPosition;
        _tiles = inTiles;

        viewGO = inViewGO;

        OnTilesChanged?.Invoke();
    }



    public Tile GetTile(Vector2DInt inPosition) => 
        _tiles[inPosition.x, inPosition.y];

    public void SetTile(Vector2DInt inPosition, Tile inTile)
    {
        _tiles[inPosition.x, inPosition.y] = inTile;
        OnTilesChanged?.Invoke();
    }
}



