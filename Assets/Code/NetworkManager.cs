using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;
using Lidgren.Network;
using static Constants;
using static Constants.Networking;


public class NetworkManager : LoadBalancingClient
{
    public static event Action OnConnectedToMasterServer;
    public static event Action OnConnectedToRoom;

    public NetworkManager()
    {
        AppId         = PHOTON_CLOUD_APP_ID;
        AppVersion    = GAME_VERSION;
        AutoJoinLobby = AUTO_JOIN_LOBBY;

        OnStateChangeAction += (ClientState inClientState) =>
        {
            switch (inClientState)
            {
                case ClientState.Joining:
                    Debug.Log("Joining room...");
                    break;

                case ClientState.Joined:
                    Debug.Log("Joined room: " + CurrentRoom.Name + ", PlayerCount: " + CurrentRoom.PlayerCount + ", ID: " + LocalPlayer.ID);
                    OnConnectedToRoom?.Invoke();
                    break;

                case ClientState.ConnectedToMasterserver:
                    Debug.Log("Connected to master server.");
                    OnConnectedToMasterServer?.Invoke();
                    break;
            }
        };

        OnConnectedToMasterServer += TryJoinOrCreateDebugRoom;

        Program.OnUpdate += Update;
        Program.OnQuit   += Disconnect;

        TryConnectToServer();
    }

    
    void Update()
    {
        Service();
    }

    public static void Send(byte[] inData) => 
        Program.networkManager.OpRaiseEvent(0, inData, true, RaiseEventOptions.Default);

    public override void OnEvent(EventData photonEvent)
    {
        base.OnEvent(photonEvent);

        if (photonEvent.Code == (byte)NetEventCode.Command)
        {
            byte[] incomingBytes = (byte[])photonEvent.Parameters[245];

            CommandManager.ProcessCommand(new NetBuffer(incomingBytes));
        }

    }


    void TryConnectToServer()
    {
        if (!ConnectToRegionMaster("eu"))
            DebugReturn(DebugLevel.ERROR, $"Can't connect to: { CurrentServerAddress }");
    }

    void TryJoinOrCreateDebugRoom()
    {
        if (!OpJoinOrCreateRoom("debugTest", new RoomOptions() { MaxPlayers = 4 }, TypedLobby.Default))
            DebugReturn(DebugLevel.ERROR, "Could not create or join room!");
    }
}

public enum NetEventCode
{
    Command,
}
