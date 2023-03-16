using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header ("Player parameters")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float velocidad = 5;

    private Rigidbody2D rb2d;
    private Collider2D col2d;
    private Animator anim;

    [Header ("Player checks collider")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = Physics2D.IsTouchingLayers(col2d,whatIsGround);
        //movimiento
        rb2d.velocity = new Vector2(velocidad, rb2d.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        anim.SetBool("Grounded", isGrounded);
    }
}
