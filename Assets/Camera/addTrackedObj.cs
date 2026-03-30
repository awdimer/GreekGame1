using UnityEngine;
using Unity.Cinemachine;

public class addTrackedObj : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject boss = GameObject.FindWithTag("targetgroup");
        GameObject.FindWithTag("targetgroup").GetComponent<CinemachineTargetGroup>().AddMember(transform, 1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
