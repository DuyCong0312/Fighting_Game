using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private Transform hitPos;

    private void SpawnEffect()
    {
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.hit, hitPos, transform.rotation);
    }
}
