using UnityEngine;
using Unity.Cinemachine;



public class addtrackedobject : MonoBehaviour
{

    void Start()
    {    
       this.gameObject.GetComponent<CinemachineTargetGroup>().AddMember(transform, 1,1);
    } 


    void Update()
    {
    
    }
}
