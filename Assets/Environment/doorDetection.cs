using UnityEngine;

public class doorDetection : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private GameObject doorSpawnPoint;
    [SerializeField] private GameObject Door;
    private bool doorSpawned = false;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && doorSpawned == false)
        {
            Instantiate(Door, doorSpawnPoint.transform.position, Quaternion.identity);
            doorSpawned = true;
        }
    }
}
