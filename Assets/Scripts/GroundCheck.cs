using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private ComMovement comMovement;

    private void OnTriggerStay2D(Collider2D collision)
    {
        comMovement.isGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        comMovement.isGround = false;
    }
}
