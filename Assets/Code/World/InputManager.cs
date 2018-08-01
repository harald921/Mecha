using System;
using UnityEngine;

public class InputManager
{
    readonly World _world;

    public event Action<Tile> OnTileClicked; // TODO: Move this to some kind of WorldInputManager

    public InputManager(World inWorld)
    {
        _world = inWorld;

        Program.OnUpdate += ReadInput;

        GameObject.FindObjectOfType<ButtonNextTurn>().OnClicked += () => new Command.EndTurn(Program.networkManager.LocalPlayer.ID).ExecuteAndSend();
    }


    void ReadInput()
    {
        // Check what (if any) Tile was clicked
        if (Input.GetMouseButtonDown(0))
        {
            Tile clickedTile = _world.GetTile(GetCurrentMouseWorldPos());

            if (clickedTile != null)
                OnTileClicked?.Invoke(clickedTile);
        }

    }


    static Vector2DInt GetCurrentMouseWorldPos()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2DInt((int)mouseWorldPosition.x, (int)mouseWorldPosition.z);
    }
}
