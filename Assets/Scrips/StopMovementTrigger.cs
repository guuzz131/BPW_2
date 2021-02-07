using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovementTrigger : MonoBehaviour
{
    public PlayerCtrl playerCtrl;

    private void Start()
    {
        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCtrl.objectIsGrounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCtrl.objectIsGrounded = false;
    }
}
