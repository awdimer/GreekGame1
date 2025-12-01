using System.Collections;
using System.Collections.Generic;

using System.Reflection;
using UnityEngine;

public class enemy_mov : MonoBehaviour
{
    public Transform [] patrolPoints;
    [SerializeField]public float moveSpeed;
    [SerializeField] public int patrolDestination;
    [SerializeField] private LayerMask groundLayer;

    private Animator anim;
    private Rigidbody2D rb; 
    private bool isPatrolling = true;
    private BoxCollider2D box;




    [SerializeField] public float KBforce; // (knockback force) how powerfull the knockback is
    [SerializeField] public float KBcounter;// how much time left on the effect
    [SerializeField] public float KBtotaltime;// how long the effect lasts
    public bool knockFromRight;

    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        
    }
    void Update()
    {
        if((isPatrolling) &&  (KBcounter <= 0) && isGrounded())
        {
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 1;
                }
            }
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 0;
                }
            }
        }
        
        else if (!isPatrolling)
        {
            if (knockFromRight == true)
            {
                rb.linearVelocity = new Vector2(-KBforce, KBforce);
            }
            if (knockFromRight == false)
            {
                rb.linearVelocity = new Vector2(KBforce, KBforce);
            }
            KBcounter -= Time.deltaTime;
            Debug.Log(KBcounter);
        }
        if(KBcounter <= 0 )
        {
            Debug.Log("Back to patrolling");
            isPatrolling = true;
        }
        
            
    }
    
    public void knockBack()
    {
        Debug.Log("knockBack");
        isPatrolling = false;

        
        KBcounter = KBtotaltime;
        

        
        
    }

    private bool isGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
