using UnityEngine;
using Lidgren.Network;

static class CommandManager
{
    public static void ProcessCommand(NetBuffer inCommandBuffer)
    {
        Command.Type commandType = (Command.Type)inCommandBuffer.ReadVariableInt32();

        switch (commandType)
        {
            case Command.Type.SpawnMech:
                new Command.SpawnMech(inCommandBuffer).Execute();
                break;

            case Command.Type.MoveMech:
                new Command.MoveMech(inCommandBuffer).Execute();
                break;

            default:
                Debug.LogError("Unknown command recieved. Did you forget to add it to the CommandManager?");
                break;
        }
    }
}
