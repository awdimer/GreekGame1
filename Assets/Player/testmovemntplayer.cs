

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class testPlayerMovement : MonoBehaviour
{
#region VARIABLES
   private Rigidbody2D rb;
   private Animator anim;
   private BoxCollider2D boxCollider;
   [SerializeField] private float velPower;
   [SerializeField] private float walkSpeed;
   [SerializeField] private float Sprintspeed;
   [SerializeField] private float acceleration;
   [SerializeField] private float startDecceleration;
   [SerializeField] private float frictionAmount;
   [SerializeField] private float jumpForce;
   [SerializeField] private float wallJumpForcex;
   [SerializeField] private float wallJumpForcey;
   [SerializeField] private float jumpBufferTime;
   [SerializeField] private float coyoteTime;
   [SerializeField] private float jumpCutMultipliery;
   [SerializeField] private float jumpCutMultiplierx;

   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private LayerMask wallLayer;
   [SerializeField] private Transform wallCheck;
   [SerializeField] public float KBforce;
   [SerializeField] private GameObject cameraFollowGO;
   [SerializeField] public Animator animator;

   private float wallJumpCooldown;
   private float moveSpeed;
   public bool knockFromRight;
   private float lastGroundedTime;
   private float lastJumpTime;
   private bool isSprinting;
   private bool isJumping;
   public bool isFacingRight = true;
   private bool isOnWall;
    private float decceleration;
    private CameraFollowObject cameraFollowObject;
    private float fallSpeedyDampingChangeThreshold;
    

    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private GameObject bulletPrefab;
    /// <summary>
    /// Combat Stuff
    /// </summary>
    [SerializeField] float parryTime;
    [SerializeField] float parryCooldown;
    [SerializeField] public int SwordDamage;
    [SerializeField] float dodgeTime;
    [SerializeField] float dodgeCoolDown;
    [SerializeField] private float dodgePower;
    public bool isParrying = false;
    public bool isDodging = false;
    private float nextReadyCooldownTime;
    
    private float horizontalInput;
   private Vector2 moveInput;
   private double previousY;
   private double currentY;
   private bool isFalling;

   private bool isAttacking;
#endregion
#region AWAKE
   private void Awake()
   {
       ///grabs references for rigidbody from game object, so it can be used in code
       rb = GetComponent<Rigidbody2D>();
       boxCollider = GetComponent<BoxCollider2D>();
       
   }
    
    void Start()
    {
        TurnCheck();
        cameraFollowObject = cameraFollowGO.GetComponent<CameraFollowObject>();
        
    }

#endregion
#region UPDATES
   private void Update()
   {
        animator.SetBool("grounded",isGrounded());
        animator.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));

        bool walking = Mathf.Abs(moveInput.x) > 0.01f && isGrounded();
        animator.SetBool("isWalking", walking);

        horizontalInput = Input.GetAxis("Horizontal");


       lastGroundedTime -= Time.deltaTime;
       lastJumpTime -= Time.deltaTime;
       wallJumpCooldown += Time.deltaTime;


       moveInput.y = Input.GetAxisRaw("Vertical");

        StartCoroutine(CheckFalling());

        if (isFalling)
        {
            animator.SetBool("isFalling",true);
        }
        if(!isFalling)
        {
            animator.SetBool("isFalling",false);
        }

       if (isGrounded())// coyote time
       {
           lastGroundedTime = coyoteTime;
           isJumping = false;
       }
       if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))// if the jump button is released the player will do a jump cut
       {
           if (rb.linearVelocity.y > 0 && isJumping)
           {
               rb.AddForce(Vector2.down * rb.linearVelocity.y * (1 - jumpCutMultipliery), ForceMode2D.Impulse);
                if (isFacingRight)
                {
                    rb.AddForce(Vector2.right * rb.linearVelocity.x * (1 - jumpCutMultiplierx), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.left * rb.linearVelocity.x * (1 - jumpCutMultiplierx), ForceMode2D.Impulse);
                }
          }
       }
       if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextReadyCooldownTime)
        {
            StartCoroutine(ParryCoroutine());
            nextReadyCooldownTime = Time.time + parryCooldown;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextReadyCooldownTime)
        {
            StartCoroutine(dodgeCoroutine());
            nextReadyCooldownTime = Time.time + dodgeCoolDown;
        }
        
        
   }
   void FixedUpdate()
   {
    if(isSprinting==true)
        {
            moveSpeed = walkSpeed + Sprintspeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

    
       if (lastJumpTime>0 && !isJumping && lastGroundedTime>0 )//check if the player can and wants to jump and runs jump
           {
               jump();
           }
       //start of left right movement
       if( isParrying == false && isDodging == false && isAttacking == false)
       {
        float targetSpeed = moveInput.x * moveSpeed ;
        float SpeedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float deccelRate = decceleration;
        float movement = Mathf.Pow(Mathf.Abs(SpeedDif) * accelRate, velPower) * Mathf.Sign(SpeedDif);
        rb.AddForce(movement * Vector2.right);
       }
        if (!isGrounded())
        {
            decceleration = 1;
        }
        else
        {
            decceleration = startDecceleration;
        }

       //end of left right movement



       if (isGrounded() && Mathf.Abs(moveInput.x) < 0.01f )
       {
           float amount = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), Mathf.Abs(frictionAmount));
           amount *= Mathf.Sign(rb.linearVelocity.x);
           rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
          
       }

       if(isDodging == true)
        {
            dodge();
        }
       




       TurnCheck();       
       if (onWall() && !isGrounded())
       {
           rb.gravityScale = 0;
           rb.linearVelocity = Vector2.zero;
       }
       else
           rb.gravityScale = 5;
       if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onWall() && !isGrounded()&& wallJumpCooldown > .2)
       {
           wallJump();
       }

       

        
    
      
  
  
   }
   
   #endregion
#region IS GROUNDED CHECK
   private bool isGrounded() //checks if the player is on the ground
   {


       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
       return raycastHit.collider != null;
   }
   #endregion
#region COLLISION
   public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("enemy"))
        {
            knockFromRight = collision.transform.position.x <= transform.position.x;
            rb.AddForce(Vector2.up * KBforce, ForceMode2D.Impulse);
             if (knockFromRight == true)
            {
                rb.AddForce(Vector2.right * KBforce, ForceMode2D.Impulse);
            }
            if (knockFromRight == false)
            {
                rb.AddForce(Vector2.left * KBforce, ForceMode2D.Impulse);
            }
            
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallJumpCooldown = 0;
        }

    }
    #endregion
#region JUMP
   private void jump() // applys jump force
   {
       isJumping = true;
       animator.SetTrigger("jump");
       rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


   }
   public void Move(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<Vector2>().x;
    }
   public void jumpinput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lastJumpTime = jumpBufferTime;
        }
    }
    public void attackinput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }
    public void dodgeinput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
        }
    }
    public void sprintinput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true; 
        }


        if (context.canceled)
        {
            isSprinting = false; 
        }
    }
    
   #endregion
#region TURNING 
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

           cameraFollowObject.callTurn();
       }
       else
       {
           Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
           transform.rotation = Quaternion.Euler(rotator);
           isFacingRight = !isFacingRight;

           cameraFollowObject.callTurn();
       }

   }
   #endregion
#region WALL DETECTION AND JUMP
   private bool onWall()
   {
      return Physics2D.OverlapCircle(wallCheck.position, .02f,wallLayer);
   }
   private void wallJump()
   {
       if (onWall() && !isGrounded())
       {
           isJumping = true;
           rb.AddForce(Vector2.up * wallJumpForcey, ForceMode2D.Impulse);
           if (isFacingRight)
           {
               rb.AddForce(Vector2.left * wallJumpForcex, ForceMode2D.Impulse);
           }
           if (!isFacingRight)
           {
               rb.AddForce(Vector2.right * wallJumpForcex, ForceMode2D.Impulse);
           }
          
       }
   }
   #endregion

   private IEnumerator ParryCoroutine()
    {
        isParrying = true;

        animator.SetBool("isParrying", true);


        yield return new WaitForSeconds(parryTime);
        animator.SetBool("isParrying", false);


        isParrying = false;
    }

    public void endAttack()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;

    }

    public void attack()
    {
        isAttacking = true;

        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameObject in enemy)
        {   
            enemyGameObject.GetComponent<enemyHealth>().TakeDamage(SwordDamage);
            enemyGameObject.GetComponent<enemy_mov>().knockBack();
            enemyGameObject.GetComponent<enemy_mov>().knockFromRight = isFacingRight;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    public void ReflectBullet(Vector2 ogVelocity)
{
    // Spawn new bullet
    GameObject reflected = Instantiate(bulletPrefab, attackPoint.transform.position, Quaternion.identity);

    // Send it exactly opposite
    PlayerBullet pb = reflected.GetComponent<PlayerBullet>();
    pb.ShootOut(ogVelocity, 1f); // 1 = same speed; >1 = faster
}

    public void dodge()
    {
        
        float dodgeDirection = 0;
        if (isFacingRight)
           {
            dodgeDirection = 1;
           }
           if (!isFacingRight)
           {
            dodgeDirection = -1;
           }
        rb.linearVelocity = new Vector2(dodgeDirection * dodgePower, rb.linearVelocity.y);
    }

    private IEnumerator dodgeCoroutine()
    {
        isDodging = true;
    animator.SetBool("isDodging", true);
    animator.SetBool("isWalking",false);

    

    yield return new WaitForSeconds(dodgeTime);

    animator.SetBool("isDodging", false);
    animator.SetBool("isWalking",true);
    isDodging = false;
    }

   

    private IEnumerator CheckFalling()
{
    float previousY = transform.position.y;
    yield return new WaitForSeconds(0.1f);
    float currentY = transform.position.y;
    isFalling = currentY < previousY;
}
   
}



