using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOnEscape : MonoBehaviour
{
    [SerializeField]private PlayerControls openMenuControl;

    public bool paused = false;
    public GameObject menu;

    private void Awake()
    {
        openMenuControl = new PlayerControls();
    }

    private void OnEnable()
    {
        openMenuControl.Enable();
    }

    private void OnDisable()
    {
        openMenuControl.Disable();
    }

    private void Update()
    {
        if (!openMenuControl.UI.OpenMenu.triggered) return;
        menu.SetActive(!menu.activeInHierarchy);
       // DeterminatePause();
    }

    private void DeterminatePause()
    {
        if (!paused)
        {
            PauseGame();
        }
        else
            ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
    }
    
    private void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
    }
}
