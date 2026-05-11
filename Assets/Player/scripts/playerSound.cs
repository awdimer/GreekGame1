using UnityEngine;

public class playerSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void playSwordSlash()
    {
        FindObjectOfType<AudioManager>().Play("sword slash");
    }
    void playSwordWindUp()
    {
        FindObjectOfType<AudioManager>().Play("sword windup");
    }
    void PlayWalk()
    {
        FindObjectOfType<AudioManager>().Play("walk");
    }
    void PlayJump()
    {
        FindObjectOfType<AudioManager>().Play("jumpup");
    }
    void Playjumpland()
    {
        FindObjectOfType<AudioManager>().Play("jumpland");
    }
}
