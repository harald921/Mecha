using System;
using UnityEngine;

public class InputManager
{
    readonly World _world;

    public event Action<Tile> OnTileClicked; 
    public event Action<Tile> OnTileHovered;


    public InputManager(World inWorld)
    {
        _world = inWorld;

        Program.OnUpdate += ReadInput;

        GameObject.FindObjectOfType<ButtonNextTurn>().OnClicked += () => new Command.EndTurn(Program.networkManager.LocalPlayer.ID).ExecuteAndSend();
    }


    void ReadInput()
    {
        Tile currentMouseoverTile = _world.GetTile(GetCurrentMouseWorldPos());

        if (currentMouseoverTile != null)
        {
            OnTileHovered?.Invoke(currentMouseoverTile);

            // Check what (if any) Tile was clicked
            if (Input.GetMouseButtonDown(0))
                OnTileClicked?.Invoke(currentMouseoverTile);
        }
    }

    static Vector2DInt GetCurrentMouseWorldPos()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2DInt((int)mouseWorldPosition.x, (int)mouseWorldPosition.z);
    }
}
