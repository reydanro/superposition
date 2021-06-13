using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePivotingObstacleController : MonoBehaviour
{

    [Tooltip("The initial direction of the rotation [-1 for left, 1 for right]")]
    public int initialRotationDirection = 1;

    [Tooltip("Angle per second")]
    public float rotationSpeed = 30.0f;

    [Tooltip("Total rotation degrees")]
    public float rotationDegrees = 90f;

    private float initialZ;
    private int currentRotationDirection;
    private float dtRotatedDegrees;

    // Start is called before the first frame update
    void Start()
    {
        initialZ = transform.eulerAngles.z;
        currentRotationDirection = initialRotationDirection;
        dtRotatedDegrees = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (dtRotatedDegrees >= rotationDegrees)
        {
            dtRotatedDegrees = 0;
            currentRotationDirection *= -1;
        }


        float rotation = rotationSpeed * currentRotationDirection * Time.deltaTime;
        dtRotatedDegrees += Math.Abs(rotation);
        transform.Rotate(new Vector3(0, 0, rotation));


        //if (transform.eulerAngles.z - initialZ >= rotationSize / 2f)
        //{
        //    rotationSpeed = -Math.Abs(rotationSpeed);
        //} else if (transform.eulerAngles.z - initialZ <= -rotationSize / 2f)
        //{
        //    rotationSpeed = Math.Abs(rotationSpeed);
        //}

        //Vector3.forward * rotationSpeed * Time.deltaTime;

        //transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
