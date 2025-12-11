

using UnityEngine;


public class testPlayerMovement : MonoBehaviour
{
#region VARIABLES
   private Rigidbody2D rb;
   private Animator anim;
   private BoxCollider2D boxCollider;
   [SerializeField] private float velPower;
   [SerializeField] private float moveSpeed;
   [SerializeField] private float acceleration;
   [SerializeField] private float startDecceleration;
   [SerializeField] private float frictionAmount;
   [SerializeField] private float jumpForce;
   [SerializeField] private float wallJumpForcex;
   [SerializeField] private float wallJumpForcey;
   [SerializeField] private float jumpBufferTime;
   [SerializeField] private float coyoteTime;
   [SerializeField] private float jumpCutMultiplier;
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private LayerMask wallLayer;
   [SerializeField] private Transform wallCheck;
   [SerializeField] public float KBforce;
   [SerializeField] private GameObject cameraFollowGO;

   private float wallJumpCooldown;
   public bool knockFromRight;
   private float lastGroundedTime;
   private float lastJumpTime;
   private bool isJumping;
   public bool isFacingRight = true;
   private bool isOnWall;
    private float decceleration;
    private CameraFollowObject cameraFollowObject;
    private float fallSpeedyDampingChangeThreshold;


   private Vector2 moveInput;
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
        fallSpeedyDampingChangeThreshold = cameramanager.instance.fallSpeedyDampingChangeThreshold;
    }

#endregion
#region UPDATES
   private void Update()
   {



       lastGroundedTime -= Time.deltaTime;
       lastJumpTime -= Time.deltaTime;
       wallJumpCooldown += Time.deltaTime;


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
       if(rb.linearVelocity.y < fallSpeedyDampingChangeThreshold && !cameramanager.instance.IsLerpingYDamping && !cameramanager.instance.LerpedFromPlayerFalling)
        {
            cameramanager.insatnce.LerpYDamping(true);

        }
        if (rb.linearVelocity.y >= 0f && !cameramanager.instance.IsLerpingYDamping && cameramanager.instance.LerpedFromPlayerFalling)
        {
            cameramanager.instance.LerpedFromPlayerFalling = false;
            cameramanager.instance.LerpYDamping(false);
        }
   }
   void FixedUpdate()
   {
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
       float deccelRate = decceleration;
       float movement = Mathf.Pow(Mathf.Abs(SpeedDif) * accelRate, velPower) * Mathf.Sign(SpeedDif);
       rb.AddForce(movement * Vector2.right);
        if (!isGrounded())
        {
            decceleration = 1;
        }
        else
        {
            decceleration = startDecceleration;
        }

       //end of left right movement



       if (isGrounded() && Mathf.Abs(moveInput.x) < 0.01f)
       {
           float amount = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), Mathf.Abs(frictionAmount));
           amount *= Mathf.Sign(rb.linearVelocity.x);
           rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
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
       rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


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
}


