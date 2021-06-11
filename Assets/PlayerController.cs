using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const int X_FORCE = 5;

    public const int X_FORCE_WHILE_JUMPING = 1;

    public const int JUMP_FORCE = 400;

    public const int MAX_VELOCITY = 5;

    public Rigidbody2D rb;

    private bool isJumping = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0)
        {
            this.isJumping = false;
        }

        if (rb.velocity.x <= MAX_VELOCITY && Input.GetKey(KeyCode.D))
        {
            if (this.isJumping)
            {
                rb.AddForce(new Vector2(X_FORCE_WHILE_JUMPING, 0));
            }
            else
            {
                rb.AddForce(new Vector2(X_FORCE, 0));
            }
        }
        else if (rb.velocity.x >= -MAX_VELOCITY && Input.GetKey(KeyCode.A))
        {
            if (this.isJumping)
            {
                rb.AddForce(new Vector2(-X_FORCE_WHILE_JUMPING, 0));
            }
            else
            {
                rb.AddForce(new Vector2(-X_FORCE, 0));
            }
        }

        if (!this.isJumping && Input.GetKeyDown(KeyCode.W))
        {
            this.isJumping = true;
            rb.AddForce(new Vector2(0, JUMP_FORCE));
        }
    }
}
