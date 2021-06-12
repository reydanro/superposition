using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacleController : MonoBehaviour
{
    [Tooltip("Angle per second")]
    public float rotationSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
