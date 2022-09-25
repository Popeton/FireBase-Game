using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        SceneManager.LoadScene(1);
        Time.timeScale = 0;
    }
    public void Replay()
    {
        Time.timeScale = 1;
        // SceneManager.LoadScene(0);
    }
}
