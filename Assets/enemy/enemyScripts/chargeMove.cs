using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class chargeMove : enemydammage
{[  Header("Charge Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[2];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        Stop();
    }


    // Update is called once per frame
    void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }

        }
    }

    private void CalculateDirections()
    {

        directions[0] = transform.right * range; //right direction
        directions[1] = -transform.right * range; //left direction
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Stop();
    }
}
