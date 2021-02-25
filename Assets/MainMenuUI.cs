using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
