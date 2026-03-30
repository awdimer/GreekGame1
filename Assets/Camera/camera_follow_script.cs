using UnityEngine;
using System.Collections;



public class CameraFollowObject : MonoBehaviour
{
       [SerializeField] private Transform playerTransform;
       [SerializeField] private float flipyRotationTime = .5f;
       private testPlayerMovement Player;
       private Coroutine turnCoroutine;
       private bool isFacingRight;

       private void Awake()
    {
        Player = playerTransform.gameObject.GetComponent<testPlayerMovement>();
        isFacingRight = Player.isFacingRight;
        transform.position = playerTransform.position;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {   

        if (playerTransform == null)
        return;
        
        transform.position = playerTransform.position;
    }
    public void callTurn()
    {
        turnCoroutine = StartCoroutine(flipYLerp());
    }
    private IEnumerator flipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flipyRotationTime)
        {
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation,endRotationAmount, elapsedTime/flipyRotationTime);
            transform.position = new Vector3(yRotation, 0f, 0f);
            yield return null;
        }

    }
    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;
        if (isFacingRight)
        {
            return 0;
        }
        else
        {
            return 180f;
        }
    }

}
