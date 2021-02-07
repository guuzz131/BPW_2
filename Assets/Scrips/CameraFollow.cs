using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    Transform playerObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerObj = player.GetChild(0);
        //transform.position = new Vector3(playerObj.position.x + 1, transform.position.y, transform.position.z); ;
    }
    void FixedUpdate()
    {
        if (playerObj != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerObj.position.x, transform.position.y, transform.position.z), 0.1f);

        }
    }
}
