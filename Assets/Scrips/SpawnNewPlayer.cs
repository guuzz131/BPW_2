using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnNewPlayer : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("PlayerObj");
    }
    public void ResetLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void ReturnLevel()
    {
        player.transform.GetChild(0).transform.position = new Vector3(0,0, player.transform.GetChild(0).transform.position.z);
        Debug.Log("Resetted Pos");
    }
}
