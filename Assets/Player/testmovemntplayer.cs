

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
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private LayerMask groundLayer;
    private float lastGroundedTime;
    private float lastJumpTime;
    private bool isJumping;
    private bool isFacingRight;

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

        Debug.Log(lastJumpTime);
        if (isGrounded())
        {
            lastGroundedTime = coyoteTime;
            isJumping = false;
        }
        if (Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.UpArrow))
        {
            if(rb.linearVelocity.y > 0 && isJumping)
            {
                rb.AddForce(Vector2.down * rb.linearVelocity.y*(1-jumpCutMultiplier), ForceMode2D.Impulse);
            }
        }

    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            lastJumpTime = jumpBufferTime;
        }
        if (lastJumpTime>0 && !isJumping && lastGroundedTime>0 )
            {
                jump();
                Debug.Log("jumping");
            }

        float targetSpeed = moveInput.x * moveSpeed;
        float SpeedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(SpeedDif) * accelRate, velPower) * Mathf.Sign(SpeedDif);
        rb.AddForce(movement * Vector2.right);





        if (lastGroundedTime > 0 && Mathf.Abs(moveInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.linearVelocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }


    }
    private bool isGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private void jump()
    {
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    private void TurnCheck()
    {

    }
    private void Turn()
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



    
}
