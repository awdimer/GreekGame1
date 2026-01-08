using UnityEngine;

public class AmbushScript : MonoBehaviour
{
    [SerializeField] private GameObject doorSpawnPoint1;
    [SerializeField] private GameObject doorSpawnPoint2;
    [SerializeField] private GameObject enemySpawnPoint1;
    [SerializeField] private GameObject enemySpawnPoint2;
    [SerializeField] private float enemyAmount;
    [SerializeField] private float ambushTime;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject doorPrefab;
    private bool amountAttack;
    private bool timedAttack;
    private BoxCollider2D box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(enemyAmount > 0)
        {
            amountAttack = true;
        }
        else if(ambushTime > 0)
        {
            timedAttack = true;
        }
        Instantiate(doorPrefab, enemySpawnPoint1.transform.position, Quaternion.identity);
        Instantiate(doorPrefab, enemySpawnPoint2.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
