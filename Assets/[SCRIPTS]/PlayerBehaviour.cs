/*  Source file name: PlayerBehaviour.cs
 *  Author's name: Jen Marc Capistrano
 *  Student number: 101218004
 *  Date last modified: 13 December 2021
 *  Program description: This script makes the player moves, does what the player needs to
 *  Revision history: 0.0.1 deleted everything and made a new one then used a much simpler code that i can understand    
 *                    
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float walljumpPowerX;
    [SerializeField] private float walljumpPowerY;


    [Header("Animation")]
    [SerializeField] private PlayerAnimationState state;


    private float wallJumpCooldown;
    private float inputX;


    public Rigidbody2D rbody;
    private Animator anim;
    private BoxCollider2D boxCollider;

    [Header("Layer")]
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
        // Get references
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        float inputJump = Input.GetAxisRaw("Jump");

        FlipSprite();

        // wall jump
        if (wallJumpCooldown > 0.2f)
        {

            // make player move left/right
            rbody.velocity = new Vector2(inputX * movementSpeed, rbody.velocity.y);
           

            // make player stuck on wall
            if (OnWall() && !isGrounded())
            {
                
                rbody.gravityScale = 0;
                rbody.velocity = Vector2.zero;
                anim.SetInteger("AnimationState", (int)PlayerAnimationState.ONWALL);
                state = PlayerAnimationState.ONWALL;
            }
            else
            {
                // revert the gravity scale back
                rbody.gravityScale = 10;
            }

            // make player jump
            if (inputJump > 0)
            {
                
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        Animate();
    }

   private void Jump()
    {
        anim.SetInteger("AnimationState", (int)PlayerAnimationState.JUMP);
        state = PlayerAnimationState.JUMP;
        if (isGrounded())
        {
           // jump up
            rbody.velocity = new Vector2(rbody.velocity.x, jumpPower);
          
        }
        else if (OnWall() && !isGrounded())
        {
            
            if (inputX == 0)
            {
                // detached to wall
                rbody.velocity = new Vector2(-Mathf.Sin(transform.localScale.x) * walljumpPowerX * 2, walljumpPowerY * 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                // clmib up the wall
                rbody.velocity = new Vector2(-Mathf.Sin(transform.localScale.x) * walljumpPowerX, walljumpPowerY);
            }
            wallJumpCooldown = 0;
        }
        
    }

    private void FlipSprite()
    {
        // flip player's sprite
        if (inputX > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Animate()
    {
        if (inputX != 0 && isGrounded())
        {
            anim.SetInteger("AnimationState", (int)PlayerAnimationState.RUN);
            state = PlayerAnimationState.RUN;
        }
        else if (inputX == 0 && isGrounded())
        {
            anim.SetInteger("AnimationState", (int)PlayerAnimationState.IDLE);
            state = PlayerAnimationState.IDLE;
        }
        if (rbody.velocity.y < -0.01f)
        {
            anim.SetInteger("AnimationState", (int)PlayerAnimationState.FALL);
            state = PlayerAnimationState.FALL;
        }
       
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, platformLayer);
        return raycastHit.collider != null;
        
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            // makes player stay on top of platform
            transform.SetParent(other.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            // resets player's transform
            transform.SetParent(null);
        }
    }
}
