using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionCheck : MonoBehaviour
{
    public Player playerScrpt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerScrpt.blockInFront = true;
            playerScrpt.block = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerScrpt.blockInFront = false;
        }
    }
}
