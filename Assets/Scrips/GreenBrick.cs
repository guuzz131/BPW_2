using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBrick : MonoBehaviour
{
    public GameObject lightObj;
    public GameObject otherObject;
    public bool isActivated;
    public List<GameObject> blocks = new List<GameObject>(); 
    public List<GameObject> friends = new List<GameObject>();
    public bool islevel5;
    bool i = true;
    float time;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WasPlayer")
        {
            isActivated = true;
            //Debug.Log(isActivated);
            lightObj.SetActive(true);
            friends.Add(collision.gameObject);
            if ((otherObject.GetComponent<GreenBrick>().isActivated) && collision.gameObject.tag == "WasPlayer")
            {
                //Debug.Log("BYE BYE");
                
            }
        }
        if (collision.transform.gameObject.layer == 10)
        {
            lightObj.SetActive(true);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "WasPlayer")
        {
            isActivated = true;
            lightObj.SetActive(true);
            //scaleDown(collision.gameObject);
            if (isActivated && otherObject.GetComponent<GreenBrick>().isActivated)
            {
                for (int t = 0; t < blocks.Count; t++)
                {
                    blocks[t].GetComponent<SpriteRenderer>().enabled = false;
                    blocks[t].GetComponent<ParticleSystem>().Play();
                }
            }
            
            if (i && isActivated && otherObject.GetComponent<GreenBrick>().isActivated)
            {
                
                StartCoroutine(DestroyStuff(collision.gameObject, 1f));
                i = false;
            }
        }
        if (collision.transform.gameObject.layer == 10)
        {
            lightObj.SetActive(true);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WasPlayer")
        {
            isActivated = false;
            Debug.Log(isActivated);
            lightObj.SetActive(false);
        }
        if (collision.transform.gameObject.layer == 10)
        {
            lightObj.SetActive(false);
        }
    }

    private void scaleDown(GameObject Collision)
    {
        time = time + Time.deltaTime;
        Collision.transform.localScale = Vector3.Lerp(Collision.transform.localScale, new Vector3(0,0,1), time);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 1), time);
        otherObject.transform.localScale = Vector3.Lerp(otherObject.transform.localScale, new Vector3(0, 0, 1), time);
    }
    private IEnumerator DestroyStuff(GameObject Collision, float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log(isActivated + " " + gameObject + " & " + otherObject.GetComponent<GreenBrick>().isActivated + " " + otherObject);
        
        if (isActivated && otherObject.GetComponent<GreenBrick>().isActivated)
        {
            if (islevel5)
            {
                for (int i = 0; i < Collision.GetComponent<FallingBlock>().neighbours.Count; i++)
                {
                    if (Collision.GetComponent<FallingBlock>().neighbours[i] != null)
                    {
                        Destroy(Collision.GetComponent<FallingBlock>().neighbours[i]);
                    }

                }
                for (int i = 0; i < otherObject.GetComponent<GreenBrick>().friends.Count; i++)
                {
                    if (otherObject.GetComponent<GreenBrick>().friends[i] != null)
                    {
                        Debug.Log(otherObject.GetComponent<GreenBrick>().friends[i]);
                        Destroy(otherObject.GetComponent<GreenBrick>().friends[i]);
                    }

                }
            }
            
            if (otherObject.GetComponent<GreenBrick>().friends[0] != Collision || !islevel5)
            {
                Destroy(Collision);
            }
            Destroy(otherObject);
            Destroy(gameObject);
        }
    }
}
