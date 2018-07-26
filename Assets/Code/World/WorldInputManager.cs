using UnityEngine;

public class WorldInputManager
{
    readonly World _world;

    public event System.Action<Tile> OnTileClicked; // TODO: Move this to some kind of WorldInputManager
    public event System.Action<int> OnPlayerFinishedTurn;

    public WorldInputManager(World inWorld)
    {
        _world = inWorld;
    }


    public void ManualUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tile clickedTile = _world.GetTile(GetCurrentMouseWorldPos());
            if (clickedTile != null)
                OnTileClicked?.Invoke(clickedTile);
        }

        if (Input.GetKeyDown(KeyCode.B))
            OnPlayerFinishedTurn?.Invoke(0);
        if (Input.GetKeyDown(KeyCode.N))
            OnPlayerFinishedTurn?.Invoke(1);
        if (Input.GetKeyDown(KeyCode.M))
            OnPlayerFinishedTurn?.Invoke(2);
    }

    public static Vector2DInt GetCurrentMouseWorldPos()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2DInt((int)mouseWorldPosition.x, (int)mouseWorldPosition.z);
    }
}
