using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not Yet Completed
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float yOffset = 4.0f;
    [SerializeField] private float maxY = 4.5f;
    [SerializeField] private float minDistance = 5.0f;
    [SerializeField] private float maxDistance = 10.0f;

    private void LateUpdate()
    {
        if (player1 == null || player2 == null)
        {
            Debug.Log("No found the player");
            return;
        }

        float xMiddle = (player1.transform.position.x + player2.transform.position.x) / 2;
        float yMiddle = ((player1.transform.position.y + player2.transform.position.y) / 2) + yOffset;

        float yFinal = (yMiddle > maxY) ? maxY : yMiddle;
        float distance = Mathf.Abs((player1.transform.position.x - player2.transform.position.x));

        if (distance < minDistance)
            distance = minDistance;
        if (distance > maxDistance)
            distance = maxDistance;

        transform.position = new Vector3(xMiddle,yFinal, -distance);
    }
}
