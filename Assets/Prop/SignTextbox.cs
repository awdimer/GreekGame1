using UnityEngine;

public class SignTextbox : MonoBehaviour
{
    [Header("Assign this from the Canvas")]
    public GameObject textboxUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textboxUI != null)
        {
            textboxUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && textboxUI != null)
        {
            textboxUI.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // Clean up safely if the object is disabled or destroyed mid-trigger
        if (textboxUI != null)
            textboxUI.SetActive(false);
    }
}