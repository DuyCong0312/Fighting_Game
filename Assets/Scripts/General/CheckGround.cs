using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius;
    public bool isGround;
    public bool isJumping = false;

    private void Update()
    {
        GroundCheck();
    }
    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer);

        isJumping = isGround ? false : true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, circleRadius);
    }

}
