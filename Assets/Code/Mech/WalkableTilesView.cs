using System.Collections.Generic;
using UnityEngine;

public class WalkableTilesView // TODO: Clean up
{
    GameObject[] _views;

    public WalkableTilesView(List<Vector2DInt> inPositions)
    {
        _views = new GameObject[inPositions.Count];

        for (int i = 0; i < inPositions.Count; i++)
        {
            GameObject tileView = GameObject.Instantiate(Resources.Load<GameObject>("Prefab_WalkableTile"));


            tileView.transform.position = new Vector3(inPositions[i].x + 0.5f, 1, inPositions[i].y + 0.5f);

            _views[i] = tileView;
        }
    }

    public void Destroy()
    {
        foreach (GameObject view in _views)
            GameObject.Destroy(view);
    }
}