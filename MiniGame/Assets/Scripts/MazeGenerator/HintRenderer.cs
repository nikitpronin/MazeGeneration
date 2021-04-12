using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HintRenderer : MonoBehaviour
{
    public MazeSpawner mazeSpawner;
    private LineRenderer _componentLineRenderer;

    private void Awake()
    {
        mazeSpawner = FindObjectOfType<MazeSpawner>();
        mazeSpawner.instanceFound = true;
        _componentLineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawPath()
    {
        var maze = mazeSpawner.Maze;
        var finishX = maze.FinishPosition.x;
        var finishY = maze.FinishPosition.y;

        var positions = new List<Vector3>();

        while ((finishX != 0 || finishY != 0) && positions.Count < 10000)
        {
            positions.Add(new Vector3(finishX * mazeSpawner.cellSize.x, finishY * mazeSpawner.cellSize.y, finishY * mazeSpawner.cellSize.z));
            
            var currentCell = maze.Cells[finishX, finishY];

            if (finishX > 0 &&
                !currentCell.WallWest &&
                maze.Cells[finishX - 1, finishY].DistanceFromStart < currentCell.DistanceFromStart)
            {
                finishX--;
            }
            else if (finishY > 0 &&
                     !currentCell.WallSouth &&
                     maze.Cells[finishX, finishY - 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                finishY--;
            }
            else if (finishX < maze.Cells.GetLength(0) - 1 &&
                     !maze.Cells[finishX + 1, finishY].WallWest &&
                     maze.Cells[finishX + 1, finishY].DistanceFromStart < currentCell.DistanceFromStart)
            {
                finishX++;
            }
            else if (finishY < maze.Cells.GetLength(1) - 1 &&
                     !maze.Cells[finishX, finishY + 1].WallSouth &&
                     maze.Cells[finishX, finishY + 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                finishY++;
            }
        }

        positions.Add(Vector3.zero);
        
        _componentLineRenderer.positionCount = positions.Count;
        _componentLineRenderer.SetPositions(positions.ToArray());
        gameObject.SetActive(false);
    }
}
