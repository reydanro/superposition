using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    public Transform[] markersToFit;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (markersToFit.Length == 0)
        {
            return;
        }
        Vector2 min = markersToFit[0].position;
        Vector2 max = markersToFit[0].position;

        foreach (Transform t in markersToFit)
        {   
            if (t.position.x < min.x)
            {
                min.x = t.position.x;
            }
            if (t.position.y < min.y)
            {
                min.y = t.position.y;
            }
            if (t.position.x > max.x)
            {
                max.x = t.position.x;
            }
            if (t.position.y > max.y)
            {
                max.y = t.position.y;
            }
        }

        cam.orthographicSize = Mathf.Max((max.x - min.x) * Screen.height / Screen.width / 2, (max.y - min.y) / 2) + 0.5f;
        cam.transform.position = new Vector3((max.x + min.x) / 2, (max.y + min.y) / 2, transform.position.z);
    }
}
