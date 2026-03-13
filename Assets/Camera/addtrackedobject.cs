using UnityEngine;
using Unity.Cinemachine;


public class addtrackedobject : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject boss = GameObject.FindWithTag("Boss");
        Debug.Log(GetComponent<CinemachineTargetGroup>());

        GameObject.FindWithTag("Boss").GetComponent<CinemachineTargetGroup>().AddEnemyToList(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
