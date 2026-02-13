using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] public CinemachineCamera[] allVirtualCameras;

    [SerializeField] private float fallPanAmount = .25f;
    [SerializeField] private float fallYPanTime = .35f;
    public float fallSpeedyDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping {get;private set;}
    public bool LerpedFromPlayerFalling {get;set;}
    public List<Transform> targets;

    private Coroutine lerpYPanCoroutine;
    private CinemachineVirtualCameraBase currentCamera;
    private CinemachineFramingTransposer framingTransposer;
    private float normYPanAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        
        Vector3 centerPoint = GetCenterPoint();
        transform.position = centerPoint;

    }
    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position,Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
    private void Awake()
    {
        if( instance == null)
        {
            instance = this;
        }
        for(int i = 0; i<allVirtualCameras.Length; i++)
        {
            if (allVirtualCameras[i].enabled)
            {
                currentCamera = allVirtualCameras[i];

                framingTransposer = currentCamera.GetCinemachineComponent(CinemachineCore.Stage.Body)as CinemachineFramingTransposer;
            }
        }
        normYPanAmount = framingTransposer.m_YDamping;
    }
    public void LerpYDamping(bool isPlayerFalling)
    {
        lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;
        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;
        if (isPlayerFalling)
        {
            endDampAmount = fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = normYPanAmount;
        }
        float elapsedTime = 0f;
        while(elapsedTime< fallYPanTime)
        {
            elapsedTime += Time.deltaTime;
            float LerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime/fallPanAmount);
            framingTransposer.m_YDamping = LerpedPanAmount;
            yield return null;
        }

        IsLerpingYDamping = false;
    }
}
