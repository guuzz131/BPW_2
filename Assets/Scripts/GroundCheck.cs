using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Player player;
    public LayerMask ground;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collided " + collision.gameObject.layer);
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 0)
        {
            player.Grounded();
            //Debug.Log("YOU COLLIDED " + collision.gameObject.layer);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 0)
        {
            player.NotGrounded();
        }

    }
}
