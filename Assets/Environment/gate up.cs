using UnityEngine;

public class gateup : MonoBehaviour
{   
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] private GameObject doorSpawnPoint;
    private bool doorSpawned = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("gashouaghsu");
        if (collider.CompareTag("Player") && doorSpawned == false)
        {
            rb.MovePosition(rb.position + Vector2.up * 20f);
            doorSpawned = true;
        }
    }
}
