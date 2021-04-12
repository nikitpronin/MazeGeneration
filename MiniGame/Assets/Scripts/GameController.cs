using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject mazeSpawner;
    [SerializeField] private GameObject hintRenderer;
    [SerializeField] private GameObject descriptionPanel;
    private MazeSpawner _generator;
    
    
    private bool _helperActive = false;
    private void Start()
    {
        _generator = mazeSpawner.GetComponent<MazeSpawner>() ;
    }

    public void Restart()
    {
        _generator.SetStartPosition();
    }
    public void GenerateNewMaze()
    {
        _generator.NewMaze();
    }

    public void GetHelp()
    {
        if (!_helperActive)
        {
            hintRenderer.SetActive(true);
            _helperActive = true;
            descriptionPanel.SetActive(true);
        }
        else
        {
            hintRenderer.SetActive(false);
            _helperActive = false;
            descriptionPanel.SetActive(false);
        }
    }
}
