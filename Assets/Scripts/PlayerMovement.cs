using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private BoxCollider2D coll;
    private Animator anim;
    
    private float dirX = 0f;

    [SerializeField] private float moveSpeed = 7f;

    [Range(1,20)]
    public float jumpVelocity;

    [SerializeField] private LayerMask jumpaleGround;

    private enum MovementState{idle,running,jumping,falling}
    private MovementState state = MovementState.idle;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed,rb.velocity.y);

        Jump();
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if(dirX < 0 )
        {
            state = MovementState.running;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (dirX > 0 )
        {
            state = MovementState.running;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y >.1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
   
    }
    
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,jumpaleGround);
    }
}
