using UnityEngine;
using System.Collections;

public class AmbushScript : MonoBehaviour
{
    [SerializeField] private Transform doorSpawnPoint1;
    [SerializeField] private Transform doorSpawnPoint2;
    [SerializeField] private Transform enemySpawnPoint1;
    [SerializeField] private Transform enemySpawnPoint2;

    [SerializeField] private int enemyAmount;
    [SerializeField] private float ambushTime;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject doorPrefab;

    private bool leftSpawn = true;

    void Start()
    {
        Instantiate(doorPrefab, doorSpawnPoint1.transform.position, Quaternion.identity);
        Instantiate(doorPrefab, doorSpawnPoint2.transform.position, Quaternion.identity);

        if (enemyAmount > 0)
        {
            StartCoroutine(AmountAttack());
        }
        else if (ambushTime > 0)
        {
            StartCoroutine(TimedAttack());
        }
    }

    IEnumerator AmountAttack()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator TimedAttack()
    {
        float timer = ambushTime;

        while (timer > 0f)
        {
            SpawnEnemy();
            timer -= 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = leftSpawn ? enemySpawnPoint1 : enemySpawnPoint2;
        Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        enemyPrefab.transform.localScale = enemyPrefab.transform.localScale;
        leftSpawn = !leftSpawn;
    }
}
