using UnityEngine;

public class Maze
{
    public MazeGeneratorCell[,] Cells;
    public Vector2Int FinishPosition;
}

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallWest = true;
    public bool WallSouth = true;

    public bool Visited = false;
    public int DistanceFromStart;
}