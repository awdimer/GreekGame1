using UnityEngine;
using UnityEngine.UI;

public class SignHoverUI : MonoBehaviour{
    public GameObject textbox;
    public Text textboxText;
    public string message;

    public string playerTag = "Player";
    
    private void Start(){
        if(textbox != null){
            textbox.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag(playerTag)){
            ShowTextbox();
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.CompareTag(playerTag)){
            HideTextbox();
        }
    }
    void ShowTextbox(){
        if(textbox != null){
            textbox.SetActive(true);
            if(textboxText != null){
                textboxText.text = message;
            }
        }
    }
    void HideTextbox(){
        if(textbox != null){
            textbox.SetActive(false);
        }
    }
}