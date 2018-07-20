using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class Mech
{
    public class PathfindingComponent : Component
    {
        public PathfindingComponent(Mech inParentMech) : base(inParentMech) { }


        public Path FindPath(Tile inDestination)
        {
            if (!CanEnter(inDestination))
                return Path.Empty;

            List<Tile> closedTiles = new List<Tile>();
            List<Tile> openTiles   = new List<Tile>() {
                mech.movementComponent.currentTile
            };

            while (openTiles.Count > 0)
            {
                Tile currentTile = GetTileWithLowestCost(openTiles);
    
                // If the current tile is the target tile, the path is completed
                if (currentTile == inDestination)
                    return RetracePath(mech.movementComponent.currentTile, inDestination);
    
                // Add all walkable neighbours to "openTiles"
                List<Tile> neighbours = currentTile.GetNeighbours();
                foreach (Tile neighbour in neighbours)
                {
                    if (closedTiles.Contains(neighbour))
                        continue;

                    if (!CanEnter(neighbour))
                        continue;

                    if (currentTile.terrain.data.terrainFlag == TerrainFlag.Difficult)
                        if (neighbour.terrain.data.terrainFlag == TerrainFlag.Difficult)
                            if (!mech.mobilityType.data.ContainsMobilityFlag(MobilityFlags.IgnoresDifficultTerrain))
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
            return Path.Empty;
        }

        public List<Tile> FindWalkableTiles(int inMaxDistance)
        {
            List<Tile> tilesWithinReach = new List<Tile>();
            List<Tile> tilesToCheck   = new List<Tile>() {
                mech.movementComponent.currentTile
            };

            while (tilesToCheck.Count > 0)
            {
                Tile currentTile = tilesToCheck[0];

                tilesToCheck.Remove(currentTile);
                tilesWithinReach.Add(currentTile);

                foreach (Tile neighbour in currentTile.GetNeighbours())
                {
                    if (!CanEnter(neighbour))
                        continue;

                    if (currentTile.terrain.data.terrainFlag == TerrainFlag.Difficult)
                        if (neighbour.terrain.data.terrainFlag == TerrainFlag.Difficult)
                            if (!mech.mobilityType.data.ContainsMobilityFlag(MobilityFlags.IgnoresDifficultTerrain))
                                continue;

                    if (tilesWithinReach.Contains(neighbour))
                        continue;

                    if (tilesToCheck.Contains(neighbour))
                        continue;

                    if (!IsInRange(neighbour, inMaxDistance))
                        continue;

                    tilesToCheck.Add(neighbour);
                }
            }

            return tilesWithinReach;
        }


        bool CanEnter(Tile inTileToEnter)
        {
            Debug.LogError("TODO: Check if there's already a mech here");

            if (inTileToEnter.terrain.data.terrainFlag == TerrainFlag.Impassable)
                if (!mech.mobilityType.data.ContainsMobilityFlag(MobilityFlags.Aerial))
                    return false;

            if (inTileToEnter.terrain.data.terrainFlag == TerrainFlag.Liquid)
                if (!mech.mobilityType.data.ContainsMobilityFlag(MobilityFlags.CanTravelLiquids))
                    return false;

            return true;
        }

        bool IsInRange(Tile inTargetTile, int inMaxRange)
        {
            if (FindPath(inTargetTile).distance > inMaxRange * 10)
                return false;

            return true;
        }


        Tile GetTileWithLowestCost(List<Tile> inTileList)
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
    
    
        Path RetracePath(Tile inStartTile, Tile inTargetTile)
        {
            List<Tile> pathTiles    = new List<Tile>();
            int        pathDistance = 0;

            Tile currentTile = inTargetTile;
            while (currentTile != inStartTile)
            {
                pathTiles.Add(currentTile);

                pathDistance += currentTile.node.DistanceTo(currentTile.node.parent);

                currentTile = currentTile.node.parent;
            }
    
            pathTiles.Reverse();
    
            return new Path(pathTiles, pathDistance);
        }
    }
}

public class Path
{
    public readonly List<Tile> tiles;
    public readonly int        distance;

    public static Path Empty => new Path(new List<Tile>(), 0);

    public Path(List<Tile> inTiles, int inDistance)
    {
        tiles    = inTiles;
        distance = inDistance;
    }
}