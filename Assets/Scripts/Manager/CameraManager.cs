using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not Yet Completed
public class CameraManager : MonoBehaviour
{
    private Transform[] playerTransforms;

    [SerializeField] private float yOffset = 2.0f;
    [SerializeField] private float minDistance = 5.0f;
    [SerializeField] private float maxDistance = 10.0f;

    private float xMin, xMax, yMin, yMax;
    private void Start()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        playerTransforms = new Transform[allPlayers.Length];

        for (int i = 0; i < allPlayers.Length; i++)
        {
            playerTransforms[i] = allPlayers[i].transform;
        }
    }

    private void LateUpdate()
    {
        if (playerTransforms.Length == 0)
        {
            Debug.Log("have no found a player, make sure the player tag is on");
            return;
        }

        xMin = xMax = playerTransforms[0].position.x;
        yMin = yMax = playerTransforms[0].position.y;

        for (int i = 1; i < playerTransforms.Length; i++)
        {
            if (playerTransforms[i].position.x < xMin)
                xMin = playerTransforms[i].position.x;

            if (playerTransforms[i].position.x > xMax)
                xMax = playerTransforms[i].position.x;

            if (playerTransforms[i].position.y < yMin)
                yMin = playerTransforms[i].position.y;

            if (playerTransforms[i].position.y > yMax)
                yMax = playerTransforms[i].position.y;
        }

        float xMiddle = (xMin + xMax) / 2;
        float yMiddle = (yMin + yMax) / 2;
        float distance = xMax - xMin;

        if (distance < minDistance)
            distance = minDistance;
        if (distance > maxDistance)
            distance = maxDistance;

        transform.position = new Vector3(xMiddle, yMiddle + yOffset, -distance);
    }
}
