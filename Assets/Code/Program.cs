using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    public static World world { get; private set; }

    public static InputManager   inputManager   { get; private set; }
    public static TurnManager    turnManager    { get; private set; }
    public static MechManager    mechManager    { get; private set; }
    public static NetworkManager networkManager { get; private set; }

    public static event Action OnUpdate;
    public static event Action OnQuit;

    void Awake()
    {
        world = new World();

        inputManager   = new InputManager(world);
        turnManager    = new TurnManager();
        mechManager    = new MechManager();
        networkManager = new NetworkManager();

        OnUpdate += () =>
        {
            if (Input.GetKeyDown(KeyCode.O))
                mechManager.GenerateDebugMechs();
        };
    }


    void Update() => OnUpdate?.Invoke();
    void OnApplicationQuit() => OnQuit?.Invoke();
}
