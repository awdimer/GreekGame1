using UnityEngine;
public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    public int playerHealth;
    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player")?.GetComponent<health_player>();
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        //if(collision.transform.tag = "Checkpoint")
        //{
          //  currentCheckpoint = collision.transform;
        //    collision.GetComponent<Collider>().enabled = false;
      //  }
    //}
}