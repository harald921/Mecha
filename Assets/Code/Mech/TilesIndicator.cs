using System.Collections.Generic;
using UnityEngine;

public class TilesIndicator 
{
    List<GameObject> _views = new List<GameObject>();

    static GameObject _prefabWalkableTile;


    static TilesIndicator()
    {
        _prefabWalkableTile = Resources.Load<GameObject>("Prefab_WalkableTile");
    }


    public TilesIndicator(List<Vector2DInt> inTilePositions)
    {
        foreach (Vector2DInt tilePosition in inTilePositions)
        {
            GameObject tileView = Object.Instantiate(_prefabWalkableTile);

            tileView.transform.position = new Vector3(tilePosition.x + 0.5f,
                                                      1,
                                                      tilePosition.y + 0.5f);

            _views.Add(tileView);
        }
    }

    public virtual void Destroy() =>
        _views.ForEach(tileView => tileView.Destroy());
}