using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float wallJumpPowerY;
    [SerializeField] private float wallJumpPowerX;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    /// <summary>
    /// Combat Stuff
    /// </summary>
    [SerializeField] float parryTime ;
    [SerializeField] float parryCooldown;
    [SerializeField] public int SwordDamage;
    public bool isParrying = false;
    private float nextReadyCooldownTime;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float wallJumpCooldown;
    private bool isWallLeft = false;
    private bool isWallRight = false;

    //knockback variables
    [SerializeField] public float KBforce; // (knockback force) how powerfull the knockback is
    [SerializeField] public float KBcounter;// how much time left on the effect
    [SerializeField] public float KBtotaltime;// how long the effect lasts
    public bool knockFromRight;
    private void Awake()
    {
        ///grabs references for rigidbody from game object, so it can be used in code
        
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        ///This is player's horozontal 


        ///i believe these two things are for turning the sprite left and right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
            anim.SetBool("isWalking", true);

        }
        ////turning left
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("isWalking", true);

        }
        else if (isGrounded())
        {
            anim.SetBool("isWalking", false);
        }
        else if (!isGrounded())
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextReadyCooldownTime)
        {
            StartCoroutine(ParryCoroutine());
            nextReadyCooldownTime = Time.time + parryCooldown;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttacking", true);
        }
        

    }
    void FixedUpdate()
    {
        if ((KBcounter <= 0) && wallJumpCooldown > 0.2f)
        {   
            if (isParrying == false)
            {
                body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            }
            

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
                
            }
            else
                body.gravityScale = 5;

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && isGrounded()))
            {
                groundJump();
            }
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onWall() && !isGrounded()))
            {
                wallJump();
            }
        }


        else
        {
            wallJumpCooldown += Time.deltaTime;

            if (knockFromRight == true && isGrounded())
            {
                body.linearVelocity = new Vector2(-KBforce, KBforce);
            }
            if (knockFromRight == false && isGrounded())
            {
                body.linearVelocity = new Vector2(KBforce, KBforce);
            }
            KBcounter -= Time.deltaTime;
        }
    }


    private bool isGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        bool facingRight = transform.localScale.x > 0;

        if (facingRight)
        {
            RaycastHit2D hitRight = Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0,
                Vector2.right,
                0.1f,
                wallLayer
            );

            isWallRight = hitRight.collider != null;
            isWallLeft = false;  // explicitly false, since facing right

            return isWallRight;
        }
        else
        {
            RaycastHit2D hitLeft = Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0,
                Vector2.left,
                0.1f,
                wallLayer
            );

            isWallLeft = hitLeft.collider != null;
            isWallRight = false;  // explicitly false, since facing left

            return isWallLeft;
        }
    }
    private void groundJump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
        }
    }

    private void wallJump()
    {
        if (isWallLeft && wallJumpCooldown > 0.2f)
        {
            // Jump right and up if on left wall
            body.linearVelocity = new Vector2(wallJumpPowerX, wallJumpPowerY);
        }
        else if (isWallRight && wallJumpCooldown > 0.2f)
        {
            // Jump left and up if on right wall
            body.linearVelocity = new Vector2(-wallJumpPowerX, wallJumpPowerY);

        }
        wallJumpCooldown = 0;
    }

    private IEnumerator ParryCoroutine()
    {
        isParrying = true;

        anim.SetBool("isParrying", true);


        yield return new WaitForSeconds(parryTime);
        anim.SetBool("isParrying", false);


        isParrying = false;
    }

    public void endAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    

    
}
