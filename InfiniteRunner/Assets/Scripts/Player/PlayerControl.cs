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
    private bool stoppedJump;
    private bool doubleJump;

    private Rigidbody2D rb2d;
    //private Collider2D col2d;
    private Animator anim;

    [Header ("Player checks collider")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private float groundCheckRadius;

    [Header("Sounds & GameManager")]
    [SerializeField] private GameManager gm;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource doubleJumpSound;
    [SerializeField] private AudioSource deathSound;



    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //col2d = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        stoppedJump = true;
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
            if (isGrounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                stoppedJump = false;
                jumpSound.Play();
            }
            if(!isGrounded && doubleJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJump = false;
                doubleJump = false;
                doubleJumpSound.Play();
            }
        }

        if((Input.GetKey (KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJump)
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
            stoppedJump = true;
        }

        if (isGrounded)
        {
            jumpTimeCounter = jumpTime;
            doubleJump = true;
        }
        anim.SetBool("Grounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Layers.DEATHZONE)
        {
            gm.RestartGame();
            deathSound.Play();
        }
            
           
    }
}
