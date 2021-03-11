using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseStuff;

    bool isPaused;
    private void Start()
    {
        pauseStuff = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseStuff.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pauseStuff.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }
    public void Play()
    {
        pauseStuff.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Debug.Log("Play");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
