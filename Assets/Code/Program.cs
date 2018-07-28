using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    public static World world { get; private set; }

    public static TurnManager  turnManager  { get; private set; }
    public static MechManager  mechManager  { get; private set; }
    public static InputManager inputManager { get; private set; }

    public static event System.Action OnUpdate;


    void Awake()
    {
        world = new World();

        turnManager  = new TurnManager();
        mechManager  = new MechManager();
        inputManager = new InputManager(world);

        mechManager.GenerateDebugMechs();
    }


    void Update() => OnUpdate?.Invoke();
}
