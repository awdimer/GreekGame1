using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] public CinemachineCamera[] allVirtualCameras;

    [SerializeField] private float fallPanAmount = .25f;
    [SerializeField] private float fallYPanTime = .35f;
    public float fallSpeedyDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping {get;private set;}
    public bool LerpedFromPlayerFalling {get;set;}

    private Coroutine lerpYPanCoroutine;
    private CinemachineVirtualCameraBase currentCamera;
    private CinemachineFramingTransposer framingTransposer;
    private float normYPanAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
