using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    public Transform batHouse;
    public float speed;
    public float distance;

    private float x;
    private float y;
    private Vector2 newPosV2;
    private float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        newPos();
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2.Distance(transform.position, newPosV2) > distance))
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosV2, Time.deltaTime * speed);

        }
        else
        {
            newPos();
            Turn();
        }
    }

    void newPos()
    {
        x = Random.Range(-3.5f, 3.5f) + batHouse.position.x;
        y = Random.Range(-3.5f, 3.5f) + batHouse.position.y;
        newPosV2 = new Vector2(x, y);
    }
    void Turn()
    {
        if (transform.position.x > newPosV2.x)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
