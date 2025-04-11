using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class KnockBack : MonoBehaviour
{
    [Header("KnockBack")]
    [SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private float hitDirectionForce = 2f;

    [Header("BlowUp")]
    [SerializeField] private float blowUpPower = 5f;

    private PlayerState playerState;
    private CheckGround groundCheck;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
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
        Vector2 hitForce = hitDirection * hitDirectionForce;
        float elapsedTime = 0f;
        while (elapsedTime < knockBackTime)
        {
            rb.velocity = new Vector2(hitForce.x, hitForce.y * 5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    private IEnumerator BlowUp(Vector2 blowDirection)
    {
        Vector2 blowForce = blowDirection * blowUpPower;
        rb.velocity = new Vector2(blowForce.x, blowForce.y);
        
        yield return null;
    }
}
