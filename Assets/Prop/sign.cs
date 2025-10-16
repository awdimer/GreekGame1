using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject textbox;

    private void Start()
    {
        // Automatically find a child object named "Textbox"
        if (textbox == null)
        {
            textbox = transform.Find("Textbox")?.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textbox != null)
        {
            textbox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textbox != null)
        {
            textbox.SetActive(false);
        }
    }

    private void Update()
    {
        if (textbox != null && textbox.activeSelf)
        {
            textbox.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        }
    }
}