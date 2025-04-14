using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectAfterImage : MonoBehaviour
{
    private float spawnDelayTime;
    [SerializeField] private float spawnDelaySeconds;
    [SerializeField] private GameObject afterImageEffect;
    public bool useEffect = false;

    private void Start()
    {
        spawnDelayTime = spawnDelaySeconds;
    }
    private void Update()
    {
        if (useEffect)
        {
            if(spawnDelayTime > 0)
            {
                spawnDelayTime -= Time.deltaTime;
            }
            else
            {
                GameObject currentAfterImage = Instantiate(afterImageEffect, this.transform.position, this.transform.rotation, this.transform);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentAfterImage.GetComponent<SpriteRenderer>().sprite = currentSprite;
                spawnDelayTime = spawnDelaySeconds;
            }
        }
    }
}
