using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;

    [SerializeField] private float fallPanAmount = .25f;
    [SerializeField] private float fallYPanTime = .35f;
    public float fallSpeedyDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping {get;private set;}
    public bool LerpedFromPlayerFalling {get;set;}

    private Coroutine lerpYPanCoroutine;
    private CinemachineVirtualCamera currentCamera;
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

                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        normYPanAmount = framingTransposer.Damping.y;
    }
    public void LerYdamping(bool isPlayerFalling)
    {
        lerpYPanCoroutine = startCoroutine(LerpYAction(isPlayerFalling));
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;
        float startDampAmount = framingTransposer.Damping.y;
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
        while(elapsedTime< fallYPanAmount)
        {
            elapsedTime += elapsedTime.deltaTime;
            float LerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime/fallPanAmount);
            framingTransposer.Damping.y = LerpedPanAmount;
            yield return null;
        }

        IsLerpingYDamping = false;
    }
}
