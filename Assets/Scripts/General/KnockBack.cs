using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [Header("KnockBack")]
    [SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private float hitDirectionForce = 5f;

    [Header("BlowUp")]
    [SerializeField] private float blowUpPower;
    [SerializeField] private float blowUpTime;

    private PlayerMovement playerMovement;
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
    public void BlowUpAction(Vector2 hitDirection)
    {
        StartCoroutine(BlowUp());
    }

    private IEnumerator KnockBackRoutine(Vector2 hitDirection)
    {
        Vector2 hitForce = hitDirection * hitDirectionForce;

        float elapsedTime = 0f;
        while (elapsedTime < knockBackTime)
        {
            rb.velocity = new Vector2(hitForce.x, hitForce.y);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    private IEnumerator BlowUp()
    {
        float direction = playerMovement.isFacingRight ? 1 : -1;

        float elapsedTime = 0f;
        while (elapsedTime < blowUpTime)
        {
            rb.velocity = new Vector2(-direction * blowUpPower, blowUpPower / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }
}
