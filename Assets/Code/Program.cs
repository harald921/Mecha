using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    static Program _instance;
    public static Program instance => _instance ?? (_instance = FindObjectOfType<Program>());

    public World world { get; private set; }

    public TurnManager  turnManager  { get; private set; }
    public MechManager  mechManager  { get; private set; }
    public InputManager inputManager { get; private set; }

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
