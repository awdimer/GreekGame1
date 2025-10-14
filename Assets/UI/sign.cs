using UnityEngine;
public class Sign : MonoBehaviour{
    public GameObject textbox;
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            textbox.SetActive(true);
        }
    }
    private void OnTriggerExit2d(Collider2D other){
        if(other.CompareTag("Player")){
            textbox.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            textbox.SetActive(false);
        }
    }
    private void Update(){
        if(textbox.activeSelf){
            Vector3 hoverPos = transform.position + new Vector3(0,1.5f,0);
            textbox.transform.position = hoverPos;
        }
    }
}
