﻿// http://www.keeganleary.com
// Copyright (c) Keegan Leary

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    void Start() {
        {
            audioManager = AudioManager.instance;
            if (audioManager == null)
            {
                Debug.LogError("No Audio Manager Found");
            }
        }
    }

    public void StartGame()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Debug.Log("We Quit the game.");
        Application.Quit();
    }

    public void OnMouseOver() 
    {
        audioManager.PlaySound(hoverOverSound);
    }
}
