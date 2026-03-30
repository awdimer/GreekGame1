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
    private Coroutine panCameraCoroutine;
    public CinemachineVirtualCameraBase currentCamera;
    private CinemachinePositionComposer positionComposer;
    private float normYPanAmount;
    private Vector3 startingTrackedObjectOffset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateCameraReference();
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
        
        //for(int i = 0; i<allVirtualCameras.Length; i++)
        //{
     //       if (allVirtualCameras[i].enabled)
      //      {
      //          currentCamera = allVirtualCameras[i];
//
       //         positionComposer = currentCamera.GetComponent<CinemachinePositionComposer>();
       //     }
       // }
       CinemachineVirtualCameraBase highest = allVirtualCameras[0];
        for (int i = 1; i < allVirtualCameras.Length; i++)
        {
            if (allVirtualCameras[i].Priority > highest.Priority)
            {
                highest = allVirtualCameras[i];
            }
        }

        currentCamera = highest;
        positionComposer = currentCamera.GetComponent<CinemachinePositionComposer>();
        normYPanAmount = positionComposer.Damping.y;

        startingTrackedObjectOffset = positionComposer.TargetOffset;
    }
    public void LerpYDamping(bool isPlayerFalling)
    {
        lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;
        float startDampAmount = positionComposer.Damping.y;
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
            positionComposer.Damping.y = LerpedPanAmount;
            yield return null;
        }

        IsLerpingYDamping = false;
    }
    public void swapCamera(CinemachineCamera cameraFromLeft,CinemachineCamera cameraFromRight,Vector2 triggerExitDirection)
    {
        if(currentCamera == cameraFromLeft && triggerExitDirection.x > 0f)
        {

            cameraFromRight.Priority = 20;
            cameraFromLeft.Priority = 10;
            currentCamera = cameraFromRight;
        }
        if(currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {

            cameraFromRight.Priority = 10;
            cameraFromLeft.Priority = 20;
            currentCamera = cameraFromRight;
        }
    }
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool pantToStartingPos)
    {
        panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, pantToStartingPos));
    }
    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool pantToStartingPos)
    {
        Vector3 endPos = Vector3.zero;
        Vector3 startingPos = Vector3.zero;
        if (!pantToStartingPos)
        {
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector3.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector3.down;
                    break;
                case PanDirection.Left:
                    endPos = Vector3.left;
                    break;
                case PanDirection.Right:
                    endPos = Vector3.right;
                    break;
                default:
                    break;
            }
            endPos*=panDistance;
            startingPos = startingTrackedObjectOffset;
            endPos += startingPos;

        }
        else
        {
            startingPos = positionComposer.TargetOffset;
            endPos = startingTrackedObjectOffset;
        }
        float elapsedTime = 0f;
        while(elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 panLerp = Vector3.Lerp(startingPos,endPos,(elapsedTime/panTime));
            positionComposer.TargetOffset = panLerp;
            yield return null;
        }
        
    }
   public void UpdateCameraReference()
    {
        CinemachineVirtualCameraBase highest = allVirtualCameras[0];
        for (int i = 1; i < allVirtualCameras.Length; i++)
        {
            if (allVirtualCameras[i].Priority > highest.Priority)
            {
                highest = allVirtualCameras[i];
            }
        }

        currentCamera = highest;
        positionComposer = currentCamera.GetComponent<CinemachinePositionComposer>();
    }

}
