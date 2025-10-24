using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float parryTime ;
    [SerializeField] float parryCooldown;

    private bool isParrying;

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

        isParrying = false;
    }
}