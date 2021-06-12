using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const int X_FORCE = 5;

    public const int X_FORCE_WHILE_JUMPING = 1;

    public const int JUMP_FORCE = 6;

    public const int MAX_VELOCITY_X = 5;

    public Rigidbody2D rb;

    private Vector2 X_FORCE_VEC_RIGHT = new Vector2(X_FORCE, 0);
    private Vector2 X_FORCE_VEC_LEFT = new Vector2(-X_FORCE, 0);
    private Vector2 X_FORCE_JUMPING_VEC_RIGHT = new Vector2(X_FORCE_WHILE_JUMPING, 0);
    private Vector2 X_FORCE_JUMPING_VEC_LEFT = new Vector2(-X_FORCE_WHILE_JUMPING, 0);
    private Vector2 JUMP_FORCE_VEC = new Vector2(0, JUMP_FORCE);

    private bool isJumping = false;
    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x <= MAX_VELOCITY_X && Input.GetKey(KeyCode.D))
        {
            Vector2 force = isJumping ? X_FORCE_JUMPING_VEC_RIGHT : X_FORCE_VEC_RIGHT;
            rb.AddForce(force);
            
        }
        else if (rb.velocity.x >= -MAX_VELOCITY_X && Input.GetKey(KeyCode.A))
        {
            Vector2 force = isJumping ? X_FORCE_JUMPING_VEC_LEFT : X_FORCE_VEC_LEFT;
            rb.AddForce(force);
        }

        if (!isJumping && isColliding && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(JUMP_FORCE_VEC, ForceMode2D.Impulse);
            isJumping = true;
        } else if (isColliding) {
            isJumping = false;
        }
    }
}
