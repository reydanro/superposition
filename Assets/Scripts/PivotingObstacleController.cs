using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotingObstacleController : MonoBehaviour
{
    [Tooltip("Angle per second")]
    public float rotationSpeed = 30.0f;

    [Tooltip("Total rotation")]
    public float rotationSize = 90f;

    private float initialZ;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        initialZ = transform.eulerAngles.z;
        currentSpeed = rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    { 
        if (transform.eulerAngles.z - initialZ >= rotationSize / 2f)
        {
            rotationSpeed = -Math.Abs(rotationSpeed);
        } else if (transform.eulerAngles.z - initialZ <= -rotationSize / 2f)
        {
            rotationSpeed = Math.Abs(rotationSpeed);
        }
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
