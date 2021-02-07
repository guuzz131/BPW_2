﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private List<Transform> childs = new List<Transform>();

    float xMax;
    float xMin;
    float yMax;
    float yMin;
    bool stillColliding;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(gameObject.transform.GetChild(i).transform);
            Vector3 childPos = childs[i].position;
        }
        xMax = childs[0].position.x;
        xMin = childs[0].position.x;
        yMax = childs[0].position.y;
        for (int i = 0; i < childs.Count; i++)
        {
            float childPosXmax = childs[i].position.x;
            if (childPosXmax > xMax)
            {
                xMax = childPosXmax;
            }
            float childPosXmin = childs[i].position.x;
            if (childPosXmin < xMin)
            {
                xMin = childPosXmin;
            }
            float childPosYmax = childs[i].position.y;
            if (childPosYmax > yMax)
            {
                yMax = childPosYmax;
            }
            float childPosYmin = childs[i].position.y;
            if (childPosYmin < yMin)
            {
                yMin = childPosYmin;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        stillColliding = true;
        if (collision.gameObject.layer == 9 && (collision.GetContact(0).point.y < yMax) && (collision.GetContact(0).point.x < xMax + .5f) && (collision.GetContact(0).point.x > xMin - .5f))
        {
            for (int i = 0; i < childs.Count; i++)
            {
                childs[i].position = new Vector3(Mathf.Round(childs[i].position.x * 2f) * 0.5f, Mathf.Round(childs[i].position.y * 2f) * 0.5f, childs[i].position.z);
                childs[i].GetComponent<SpriteRenderer>().color = Color.white / 1.5f;
            }
            if (stillColliding)
            {
                Destroy(gameObject.GetComponent<Rigidbody2D>());
            }
                
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exited");
        stillColliding = false;
    }
}