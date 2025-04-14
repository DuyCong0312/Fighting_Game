using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    private Transform[] playerTransforms;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float yOffset = 4.0f;
    [SerializeField] private float maxY = 4.5f;
    [SerializeField] private Transform leftBoundaryMap;
    [SerializeField] private Transform rightBoundaryMap;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForPlayers());
    }

    private IEnumerator WaitForPlayers()
    {
        while (GameObject.FindGameObjectsWithTag("Player").Length < 1 || GameObject.FindGameObjectsWithTag("Com").Length < 1)
        {
            yield return null; 
        }

        FindPlayers(); 
    }

    private void FindPlayers()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] comFighter = GameObject.FindGameObjectsWithTag("Com");

        playerTransforms = new Transform[2];
        if (allPlayers.Length >= 2)
        {
            playerTransforms[0] = allPlayers[0].transform;
            playerTransforms[1] = allPlayers[1].transform;
        }
        else
        {
            playerTransforms[0] = allPlayers[0].transform;
            playerTransforms[1] = comFighter[0].transform;
        }

    }

    private void LateUpdate()
    {
        MoveBG();
    }

    private void MoveBG() 
    {
        float xMiddle = (playerTransforms[0].transform.position.x + playerTransforms[1].transform.position.x) / 2;
        float yMiddle = ((playerTransforms[0].transform.position.y + playerTransforms[1].transform.position.y) / 2) + yOffset;

        float yFinal = (yMiddle > maxY) ? maxY : yMiddle;

        float halfWidth = spriteRenderer.bounds.extents.x;
        float clampedX = Mathf.Clamp(xMiddle, leftBoundaryMap.position.x + halfWidth, rightBoundaryMap.position.x - halfWidth);

        this.transform.localPosition = new Vector3(clampedX, yFinal, transform.position.z);
    }
}
