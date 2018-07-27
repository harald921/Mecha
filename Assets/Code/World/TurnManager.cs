using UnityEngine;

public class TurnManager
{
    int playerTurn = 0;

    public event System.Action<int> OnNewTurn;
    public event System.Action OnNewMyTurn;

    public TurnManager()
    {
        World.instance.inputManager.OnPlayerFinishedTurn += (int inPlayerID) =>
        {
            if (inPlayerID == playerTurn)
            {
                ProgressTurn();
                OnNewTurn?.Invoke(playerTurn);
            }
        };

        OnNewTurn += (int inPlayerID) => Debug.Log("Turn changed");
    }

    void ProgressTurn()
    {
        playerTurn++;

        if (playerTurn > 2)
            playerTurn = 0;
    }
}
