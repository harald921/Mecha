using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Pathfinder 
{
    public static List<Tile> FindPath(Tile inStart, Tile inDestination)
    {
        List<Tile> closedTiles = new List<Tile>();
        List<Tile> openTiles = new List<Tile>() {
            inStart
        };

        while (openTiles.Count > 0)
        {
            Tile currentTile = GetTileWithLowestCost(openTiles);

            // If the current tile is the target tile, the path is completed
            if (currentTile == inDestination)
                return RetracePath(inStart, inDestination);

            // Add all walkable neighbours to "openTiles"
            List<Tile> neighbours = currentTile.GetNeighbours();
            foreach (Tile neighbour in neighbours)
            {
                if (!neighbour.terrain.data.passable)
                    continue;

                if (closedTiles.Contains(neighbour))
                    continue;

                // Calculate the neighbours cost from start
                int newNeighbourCostToStart = currentTile.node.costToStart + currentTile.node.CostBetween(neighbour);

                // If open tiles contains the neighbour and the new cost to start is higher than the existing, skip
                if (openTiles.Contains(neighbour))
                    if (newNeighbourCostToStart > neighbour.node.costToStart)
                        continue;

                // Since this is either a newly discovered tile or a tile with now better score, set all the node data and update the parent
                neighbour.node.costToStart   = newNeighbourCostToStart;
                neighbour.node.distanceToEnd = neighbour.node.DistanceTo(inDestination);
                neighbour.node.parent        = currentTile;

                // If it's newly discovered, add it as an open tile
                if (!openTiles.Contains(neighbour))
                    openTiles.Add(neighbour);
            }

            // This tile is now closed...
            closedTiles.Add(currentTile);
            openTiles.Remove(currentTile);
        }

        // If this is reached, no path was found. Return an empty list.
        return new List<Tile>();
    }


    static Tile GetTileWithLowestCost(List<Tile> inTileList)
    {
        Tile currentTile = inTileList[0];

        foreach (Tile openTile in inTileList)
            if (openTile.node.totalCost <= currentTile.node.totalCost)
            {
                // If the open tile has the same total cost but is further away from the end, skip it
                if (openTile.node.totalCost == currentTile.node.totalCost)
                    if (openTile.node.distanceToEnd > currentTile.node.distanceToEnd)
                        continue;

                currentTile = openTile;
            }

        return currentTile;
    }


    static List<Tile> RetracePath(Tile inStartTile, Tile inTargetTile)
    {
        List<Tile> path = new List<Tile>();

        Tile currentTile = inTargetTile;
        while (currentTile != inStartTile)
        {
            path.Add(currentTile);
            currentTile = currentTile.node.parent;
        }

        path.Reverse();

        return path;
    }
}