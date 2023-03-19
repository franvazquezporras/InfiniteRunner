using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header ("Player parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float speedIncreaseMilestone;
    private float speedMilestoneCount;
    [SerializeField] private float jumpForce;    
    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;

    private Rigidbody2D rb2d;
    //private Collider2D col2d;
    private Animator anim;

    [Header ("Player checks collider")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private float groundCheckRadius;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //col2d = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;

    }
    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius,whatIsGround);
        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone += speedIncreaseMilestone * speedMultiplier;
            speed = speed * speedMultiplier;
        }
        //movimiento
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(isGrounded)
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        if(Input.GetKey (KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(jumpTimeCounter > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }                
        }
             
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
        }

        if (isGrounded)
        {
            jumpTimeCounter = jumpTime;
        }
        anim.SetBool("Grounded", isGrounded);
    }
}
