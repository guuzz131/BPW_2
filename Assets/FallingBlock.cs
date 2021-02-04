using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private List<Transform> childs = new List<Transform>();

    float xMax;
    float xMin;
    float yMax;
    float yMin;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(gameObject.transform.GetChild(i).transform);
            Vector3 childPos = childs[i].position;
            Debug.Log("Child: " + i + " Child Pos: " + childPos);
        }
        for (int i = 0; i < childs.Count; i++)
        {
            float childPosXmax = childs[i].position.x;
            if (childPosXmax > xMax)
            {
                xMax = childPosXmax;
            }
            float childPosXmin = childs[i].position.x;
            if (childPosXmin > xMin)
            {
                xMin = childPosXmin;
            }
            float childPosYmax = childs[i].position.y;
            if (childPosYmax > yMax)
            {
                yMax = childPosYmax;
            }
            float childPosYmin = childs[i].position.y;
            if (childPosYmin > yMin)
            {
                yMin = childPosYmin;
            }
        }
        Debug.Log("XMax: " + xMax + " XMin: " + xMin + " YMax: " + yMax);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && (collision.GetContact(0).point.y < yMax) && (collision.GetContact(0).point.x < xMax) && (collision.GetContact(0).point.x > xMin))
        {
            
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }
    }
}
