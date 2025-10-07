using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

private void Awake()
    {
        ///grabs references for rigidbody from game object, so it can be used in code
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    
    private void Update()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");
        ///This is player's horozontal 
        body.linearVelocity = new Vector2(horizontalInput * speed,body.linearVelocity.y);
        
        
        ///i believe these two things are for turning the sprite left and right
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        ////turning left
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
                Jump();
        }
    }
    
    
    private void Jump()
    {
        if(isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            
        }
    }
    
    private bool isGrounded()
    {
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
}