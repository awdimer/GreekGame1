using UnityEngine;

public class AmbushTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ambushPrefab;
    [SerializeField] private Transform spawnPos;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            SpawnAmbush();
        }
    }

    private void SpawnAmbush()
    {
        Instantiate(ambushPrefab, spawnPos.position, Quaternion.identity);
    }
}