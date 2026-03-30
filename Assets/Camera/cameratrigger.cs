using UnityEngine;
using Unity.Cinemachine;
using UnityEditor;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects = new CustomInspectorObjects();
    private Collider2D coll;
    private void Start()
    {
        coll = GetComponent<Collider2D>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(customInspectorObjects.panDistance,customInspectorObjects.panTime,customInspectorObjects.panDirection, false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 exitDirection = (collision.transform.position - coll.bounds.center).normalized;
            if(customInspectorObjects.swapCameras && customInspectorObjects.cameraOnLeft !=null && customInspectorObjects.cameraOnRight != null)
            {
                CameraManager.instance.swapCamera(customInspectorObjects.cameraOnLeft,customInspectorObjects.cameraOnRight,exitDirection);
            }
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(customInspectorObjects.panDistance,customInspectorObjects.panTime,customInspectorObjects.panDirection, true);
            }
        }
    }
    
}
[System.Serializable]
public class CustomInspectorObjects
{
    [SerializeField] public bool swapCameras = false;
    [SerializeField] public bool panCameraOnContact=false;
    [HideInInspector] public CinemachineCamera cameraOnLeft;
    [HideInInspector] public CinemachineCamera cameraOnRight;
    [HideInInspector] public float panDistance=3f;
    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panTime = .35f;


}
public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}
#if UNITY_EDITOR
[CustomEditor(typeof(CameraControlTrigger))]
public class MyScriptEditor : Editor
{
    CameraControlTrigger cameraControlTrigger;
    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (cameraControlTrigger.customInspectorObjects.swapCameras)
        {
            cameraControlTrigger.customInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField("camera on left", cameraControlTrigger.customInspectorObjects.cameraOnLeft,typeof(CinemachineCamera),true) as CinemachineCamera;
            cameraControlTrigger.customInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField("camera on right", cameraControlTrigger.customInspectorObjects.cameraOnRight,typeof(CinemachineCamera),true) as CinemachineCamera;

        }
        if (cameraControlTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraControlTrigger.customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup("camera pan direction",cameraControlTrigger.customInspectorObjects.panDirection);
            cameraControlTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("pan distance",cameraControlTrigger.customInspectorObjects.panDistance);
            cameraControlTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("pan time",cameraControlTrigger.customInspectorObjects.panTime);

        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}
#endif
