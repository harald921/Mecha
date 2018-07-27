using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    public World world { get; private set; }

    public TurnManager turnManager { get; private set; }
    public MechManager mechManager { get; private set; }


    void Awake()
    {
        world = new World();

        turnManager = new TurnManager();
        mechManager = new MechManager();

        mechManager.GenerateDebugMechs();
    }


    void Update()
    {
        world.ManualUpdate();
    }
}
