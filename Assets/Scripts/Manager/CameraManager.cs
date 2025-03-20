using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not Yet Completed
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomLimiter = 2f;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        MoveCamera();
        ZoomCamera();
    }

    void MoveCamera()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, new Vector3(midpoint.x, midpoint.y, transform.position.z), smoothSpeed);
        transform.position = smoothPosition;
    }

    void ZoomCamera()
    {
        float distance = Vector3.Distance(player1.position, player2.position);
        float newZoom = Mathf.Clamp(minZoom + distance / zoomLimiter, minZoom, maxZoom);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newZoom, Time.deltaTime);
    }
}
