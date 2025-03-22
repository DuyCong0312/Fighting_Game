using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not Yet Completed
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    public float minZoom = 1f;
    public float maxZoom = 5f;
    public float zoomLimiter = 2f;
    public float maxHeightOffset = 3f;
    public float minHeightOffset = 1f;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        float distance = Vector3.Distance(player1.position, player2.position);

        float newZoom = Mathf.Clamp(minZoom + distance / zoomLimiter, minZoom, maxZoom);
        cam.orthographicSize = newZoom;

        float heightAdjust = Mathf.Lerp(minHeightOffset, maxHeightOffset, (cam.orthographicSize - minZoom) / (maxZoom - minZoom));
        Vector3 targetPosition = new Vector3(midpoint.x, midpoint.y + heightAdjust, transform.position.z);
        transform.position = targetPosition;
    }

}
