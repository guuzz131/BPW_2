using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerCtrl player;
    public LayerMask ground;
    int layerInt;
    private void Start()
    {
        player = GameObject.Find("PlayerObj").GetComponent<PlayerCtrl>();
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, ground);
        if (hit.collider != null)
        {
            player.Grounded();
        }
        else
        {
            player.NotGrounded();
        }
    }
}
