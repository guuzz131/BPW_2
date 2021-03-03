using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public GameObject laserEnd;
    private LineRenderer laser;


    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
        laserEnd.transform.position = hit.point;
        if (hit.collider != null && hit.transform.gameObject.layer != 10)
        {
            laser.SetPosition(1, hit.point);
        }
        else if (hit.collider != null && hit.transform.gameObject.layer == 10)
        {
            hit.transform.gameObject.transform.position = new Vector3(0,0,hit.transform.position.z);
        }

        else laser.SetPosition(1, transform.right * 5000);
    }
}
