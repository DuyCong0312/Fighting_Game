using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [Header("KnockBack")]
    [SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private float hitDirectionForce = 5f;

    [Header("BlowUp")]
    [SerializeField] private float blowUpPower = 5f;

    private PlayerMovement playerMovement;
    private CheckGround groundCheck;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void KnockBackAction(Vector2 hitDirection)
    {
        StartCoroutine(KnockBackRoutine(hitDirection));
    }
    public void BlowUpAction(Vector2 blowDirection)
    {
        StartCoroutine(BlowUp(blowDirection));
    }

    private IEnumerator KnockBackRoutine(Vector2 hitDirection)
    {
        float direction = playerMovement.isFacingRight ? 1 : -1;
        Vector2 hitForce = hitDirection * hitDirectionForce;
        float elapsedTime = 0f;
        while (elapsedTime < knockBackTime)
        {
            rb.velocity = new Vector2(direction * hitForce.x, hitForce.y * 5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    private IEnumerator BlowUp(Vector2 blowDirection)
    {
        float direction = playerMovement.isFacingRight ? 1 : -1;
        Vector2 blowForce = blowDirection * blowUpPower;
        rb.velocity = new Vector2(- direction * blowForce.x, blowForce.y);
        
        yield return null;
    }
}
