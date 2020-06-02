// http://www.keeganleary.com
// Copyright (c) Keegan Leary

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("We Quit the game.");
        Application.Quit();
    }
}
