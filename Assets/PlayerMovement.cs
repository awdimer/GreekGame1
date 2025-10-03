using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

private void Awake()
    {
        ///grabs references for rigidbody from game object
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    
    private void Update()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");
        
        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);
        
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
    }
    
}