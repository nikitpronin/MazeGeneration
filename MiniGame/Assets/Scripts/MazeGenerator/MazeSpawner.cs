using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class MazeSpawner : MonoBehaviour
{
    public bool instanceFound;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform playerPrefab = null;

    [Header("TPS Camera")]
    [SerializeField] private CinemachineFreeLook freeLook;

    public Vector3 cellSize = new Vector3(1,1,0);

    public HintRenderer hint;
    public Transform player = null;
    public Maze Maze;

    internal Vector3 StartPosition;
    private void Awake()
    {
        DrawMaze();
    }

    public void NewMaze()
    {
        DisposeMaze();
        DrawMaze();
    }
    private void DrawMaze()
    {
        var isPlayerActive = false; 
        var generator = gameObject.GetComponent<MazeGenerator>();
        Maze = generator.GenerateMaze();

        var rng = new System.Random();
        var width = Maze.Cells.GetLength(0);
        var height = Maze.Cells.GetLength(1);
            
        
        var playerX = rng.Next(1, width - 1);
        var playerZ = rng.Next(1, height - 1);
        
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if(!isPlayerActive && playerX == x && playerZ == y)
                {
                    StartPosition = new Vector3(x * cellSize.x, 1f, y * cellSize.z);
                    player = Instantiate(playerPrefab, StartPosition,Quaternion.identity,transform);
                    isPlayerActive = true;
                }
                
                var c = Instantiate(cellPrefab, new Vector3(x * cellSize.x, y * cellSize.y, y * cellSize.z), Quaternion.identity,transform);
                
                c.westWall.SetActive(Maze.Cells[x, y].WallWest);
                c.southWall.SetActive(Maze.Cells[x, y].WallSouth);
            }
        }
        freeLook.m_Follow = player;
        freeLook.m_LookAt = player;
        hint.DrawPath();
    }

    public void SetStartPosition()
    {
        var controller = player.GetComponent<PlayerController>();
        controller.enabled = false;
        player.position = StartPosition;
        controller.enabled = true;
    }
    private static void DisposeMaze()
    {
        var walls = GameObject.FindGameObjectsWithTag("Generated");
        foreach (var wall in walls) {
            Destroy(wall);
        }
    }
}
