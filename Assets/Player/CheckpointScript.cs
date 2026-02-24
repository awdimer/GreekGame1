using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private RespawnScript respawn;

    [HideInInspector]
    public bool hasDiscovered = false; // ← unique per checkpoint

    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn")
            .GetComponent<RespawnScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            respawn.respawnPoint = this.gameObject;

            // only trigger popup the FIRST time for THIS checkpoint
            if (!hasDiscovered)
            {
                hasDiscovered = true;
                Discover(); // call your popup logic
            }
        }
    }

    private void Discover()
    {
        
        FindObjectOfType<YourPopupManager>().discoverPopUp();
    }
}