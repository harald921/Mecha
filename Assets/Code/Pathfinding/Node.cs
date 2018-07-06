using UnityEngine;

public class Node
{
    public readonly Tile owner;
    public Tile parent;

    public int costToStart;
    public int distanceToEnd;

    public int totalCost => costToStart + distanceToEnd;


    public int DistanceTo(Tile inTargetTile)
    {
        int distanceX = Mathf.Abs(owner.worldPosition.x - inTargetTile.worldPosition.x);
        int distanceY = Mathf.Abs(owner.worldPosition.y - inTargetTile.worldPosition.y);

        return (distanceX > distanceY) ? 14 * distanceY + 10 * (distanceX - distanceY) :
                                         14 * distanceX + 10 * (distanceY - distanceX);
    }

    public int CostBetween(Tile inTargetTile) =>
        Mathf.RoundToInt(DistanceTo(inTargetTile));


    public Node(Tile inOwner)
    {
        owner = inOwner;
    }
}