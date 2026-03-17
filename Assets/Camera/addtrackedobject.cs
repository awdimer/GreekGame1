using UnityEngine;
using Unity.Cinemachine;
using System.Linq.Expressions;



public class addtrackedobject : MonoBehaviour
{
    void Awake()
    {
        GameObject[] boss = GameObject.FindGameObjectsWithTag("Boss");
    }
    void Start()
    {    
        GetComponent<CinemachineTargetGroup>().AddMember(transform, 1,1);
    } 


    void Update()
    {
        //if(boss == null)
       // {
          //  GetComponent<CinemachineTargetGroup>().RemoveMember(transform);
        //}
    }
}
