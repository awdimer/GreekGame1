using UnityEngine;

public class BossArena : MonoBehaviour
{
    [SerializeField] private Transform doorSpawnPoint1;
    [SerializeField] private Transform doorSpawnPoint2;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject bossPrefab;
    private BoxCollider2D box;
    private bool playerPresent;
    private bool bossPresent;
    private GameObject door1Instance;
    private GameObject door2Instance;
    private GameObject bossInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SpawnBossArena();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
            OpenArena();
    }
    

    private void SpawnBossArena()
    {
        door1Instance = Instantiate(doorPrefab, doorSpawnPoint1.position, Quaternion.identity);
        door2Instance = Instantiate(doorPrefab, doorSpawnPoint2.position, Quaternion.identity);
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
    }

    private void OpenArena()
    {
        if (door1Instance != null)
            Destroy(door1Instance);

        if (door2Instance != null)
            Destroy(door2Instance);
    }   
}
