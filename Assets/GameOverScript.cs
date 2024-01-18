using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject YouWinCanvas;

    internal void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Invoke("ReloadScene", 5f);
    }

    public void Win()
    {
        YouWinCanvas.SetActive(true);
        Invoke("ReloadScene", 10f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
