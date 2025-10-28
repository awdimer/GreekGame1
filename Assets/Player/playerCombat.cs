using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float parryTime ;
    [SerializeField] float parryCooldown;

    [SerializeField] public int SwordDamage;

    public bool isParrying = false;

    private float nextReadyCooldownTime;
    private Animator anim;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextReadyCooldownTime)
        {
            StartCoroutine(ParryCoroutine());
            nextReadyCooldownTime = Time.time + parryCooldown;
        }
    }

    private IEnumerator ParryCoroutine()
    {
        isParrying = true;
        
        anim.SetTrigger("parry");


        yield return new WaitForSeconds(parryTime);
        anim.SetTrigger("returnIdle");
        
        
        isParrying = false;
    }
}