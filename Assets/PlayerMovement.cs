using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    private const float SPEED = 45f;

    private float xSpeed = 0f; // -1 left, 1 right, 0 stand
    private bool jump = false;

    // Update is called once per frame
    void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal") * SPEED;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(xSpeed * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
