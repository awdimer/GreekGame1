

using UnityEngine;

public class testPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private float velPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float frictionAmount;
    [SerializeField] private float jumpForce;
    [SerializeField] private float wallJumpPowerY;
    [SerializeField] private float wallJumpPowerX;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] public float KBforce; 
    [SerializeField] public float KBcounter;
    [SerializeField] public float KBtotaltime;
    private float wallJumpCooldown;
    public bool knockFromRight;
    private float lastGroundedTime;
    private float lastJumpTime;
    private bool isJumping;
    private bool isFacingRight;
    private bool isWallRight = false;

    private Vector2 moveInput;

    private void Awake()
    {
        ///grabs references for rigidbody from game object, so it can be used in code
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (isGrounded())// coyote time
        {
            lastGroundedTime = coyoteTime;
            isJumping = false;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))// if the jump button is released the player will do a jump cut
        {
            if (rb.linearVelocity.y > 0 && isJumping)
            {
                rb.AddForce(Vector2.down * rb.linearVelocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            }
        }

    }
    void FixedUpdate()
    {
        //Debug.Log(wallJumpCooldown);
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){// if trying to jump starts a countdown and if the player is grounded during the timer it will start the jump
            lastJumpTime = jumpBufferTime;
        }
        if (lastJumpTime>0 && !isJumping && lastGroundedTime>0 )//check if the player can and wants to jump and runs jump
            {
                jump();
            }
        //start of left right movement
        float targetSpeed = moveInput.x * moveSpeed;
        float SpeedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(SpeedDif) * accelRate, velPower) * Mathf.Sign(SpeedDif);
        rb.AddForce(movement * Vector2.right);
        //end of left right movement



        if (lastGroundedTime > 0 && Mathf.Abs(moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.linearVelocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }


        TurnCheck();
        wallJumpCooldown += Time.deltaTime;
        if(wallJumpCooldown > 0.2)
        {
            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
            }
            else
                rb.gravityScale = 5;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onWall() && !isGrounded())
            {
                wallJump();
            }
        }
        
    

    }
    private bool isGrounded() //checks if the player is on the ground
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private void jump() // applys jump force
    {
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    private void TurnCheck() // checks if the player needs to turn
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            Turn();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Turn();
        }
    }
    private void Turn() //turns the player
    {
        if (isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
    }
    private bool onWall()
    {
        Debug.Log(isWallRight);
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

            isWallRight = hitLeft.collider != null;
            isWallRight = false;  // explicitly false, since facing left

            return isWallRight;
        }
    }
    private void wallJump()
    {
        if (!isWallRight && wallJumpCooldown > 0.2f)
        {
            // Jump right and up if on left wall
            rb.linearVelocity = new Vector2(wallJumpPowerX, wallJumpPowerY);
        }
        else if (isWallRight && wallJumpCooldown > 0.2f)
        {
            // Jump left and up if on right wall
            rb.linearVelocity = new Vector2(-wallJumpPowerX, wallJumpPowerY);

        }
        wallJumpCooldown = 0;
    }




    
}
