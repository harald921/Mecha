using UnityEngine;
using ExitGames.Client.Photon.LoadBalancing;
using Lidgren.Network;


public class TurnManager
{
    int currentPlayerTurn = 1;

    public event System.Action<int> OnNewTurn;
    public event System.Action OnNewMyTurn;


    public TurnManager()
    {
        OnNewTurn += (int inPlayerID) => Debug.Log("Player turn: " + currentPlayerTurn);
        OnNewMyTurn += () => Debug.Log("It's my turn!");
    }


    public void EndTurn(int inPlayerID)
    {
        if (currentPlayerTurn != inPlayerID)
            throw new System.Exception("A player tried to end turn despite not being its turn. Desync or cheater!");

        currentPlayerTurn++;

        if (currentPlayerTurn > Program.networkManager.CurrentRoom.PlayerCount)
            currentPlayerTurn = 1;

        OnNewTurn?.Invoke(currentPlayerTurn);

        if (currentPlayerTurn == Program.networkManager.LocalPlayer.ID)
            OnNewMyTurn?.Invoke();
    }
}


partial class Command
{
    public class EndTurn : Command
    {
        public override Type type => Type.EndTurn;

        public int idOfPlayerEndingTurn;


        public EndTurn(int inIdOfPlayerEndingTurn)
        {
            idOfPlayerEndingTurn = inIdOfPlayerEndingTurn;
        }

        public EndTurn(NetBuffer inCommandData)
        {
            UnpackFrom(inCommandData);
        }

        public override void Execute()
        {
            Program.turnManager.EndTurn(idOfPlayerEndingTurn);
        }


        public override int GetPacketSize() =>
            NetUtility.BitsToHoldUInt((uint)idOfPlayerEndingTurn);

        public override void PackInto(NetBuffer inBuffer) =>
            inBuffer.WriteVariableInt32(idOfPlayerEndingTurn);

        public override void UnpackFrom(NetBuffer inBuffer) =>
            idOfPlayerEndingTurn = inBuffer.ReadVariableInt32();
    }
}