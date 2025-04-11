using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    private Transform[] playerTransforms;

    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float zoomLimiter = 2f;
    [SerializeField] private float maxHeightOffset = 3f;
    [SerializeField] private float minHeightOffset = 1f;
    [SerializeField] private Transform leftBoundaryMap;
    [SerializeField] private Transform rightBoundaryMap;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
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
        if ( allPlayers.Length >= 2)
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
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 midpoint = (playerTransforms[0].position + playerTransforms[1].position) / 2f;
        float distance = Vector3.Distance(playerTransforms[0].position, playerTransforms[1].position);

        float newZoom = Mathf.Clamp(minZoom + distance / zoomLimiter, minZoom, maxZoom);
        cam.orthographicSize = newZoom;

        float heightAdjust = Mathf.Lerp(minHeightOffset, maxHeightOffset, (cam.orthographicSize - minZoom) / (maxZoom - minZoom));
        Vector3 targetPosition = new Vector3(midpoint.x, midpoint.y + heightAdjust, transform.position.z);

        float camHalfWidth = cam.orthographicSize * cam.aspect;
        float clampedX = Mathf.Clamp(targetPosition.x, leftBoundaryMap.position.x + camHalfWidth, rightBoundaryMap.position.x - camHalfWidth);

        transform.position = new Vector3(clampedX, targetPosition.y, targetPosition.z);
    }

}
