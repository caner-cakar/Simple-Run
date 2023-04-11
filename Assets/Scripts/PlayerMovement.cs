using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private BoxCollider2D coll;
    private Animator anim;
    
    private bool facingRight = true;
    private float dirX = 0f;

    [SerializeField] private float moveSpeed = 7f;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    [Range(1,20)]
    public float jumpVelocity;

    [SerializeField] private LayerMask jumpaleGround;
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

        if(dirX < 0 )
        {
            anim.SetBool("running",true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (dirX > 0 )
        {
            anim.SetBool("running",true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else 
        {
            anim.SetBool("running",false);
        }

    }  
    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            isJumping=true;
            jumpTimeCounter=jumpTime;
            rb.velocity = Vector2.up * jumpVelocity;
        }
   
        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpVelocity;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
    
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,jumpaleGround);
    }
    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
}
