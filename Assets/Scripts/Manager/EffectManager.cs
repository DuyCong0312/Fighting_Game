using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject jump;
    public GameObject groundDash;
    public GameObject airDash;
    public GameObject touchGround;

    public void SpawnEffect(GameObject name, Transform pos, Quaternion rot)
    {
        Instantiate(name, pos.position, rot);
    }
}
