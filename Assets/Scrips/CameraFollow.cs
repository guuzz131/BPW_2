using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public bool camHasArrived = false;
    public bool moveCam;

    Transform playerObj;
    bool isMoving;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (player.childCount > 0)
        {
            playerObj = player.GetChild(0);
        }

        if (moveCam)
        {
            Invoke("MoveCamBack", 1f);
        }
    }
    void FixedUpdate()
    {
        if (playerObj != null && !moveCam)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerObj.position.x, transform.position.y, transform.position.z), 0.1f);

        }
    }
    public void MoveCamBack()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,0), Time.deltaTime *2);
        //Vector3.MoveTowards(transform.position, new Vector3(0,0,0), 10);
        if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) < 1)
        {
            camHasArrived = true;
        }
    }
}
