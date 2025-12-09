using UnityEngine;
using System.Collections;


public class cameraFollowScript : MonoBehaviour
{
       [SerializeField] private Transform playerTransform;
       [SerializeField] private float flipyRotationTime = 0.3f;
       [SerializeField] private float flipRotationTime = 0.3f;
       private testPlayerMovement Player;
       private Coroutine turnCoroutine;
       private bool isFacingRight;

       private void Awake()
    {
        Player = playerTransform.gameObject.GetComponent<testPlayerMovement>();
        isFacingRight = Player.isFacingRight;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
    }
    public void callTurn()
    {
        turnCoroutine = StartCoroutine(flipYLerp());
    }
    private IEnumerator flipYLerp()
    {
        float startRotation = playerTransform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flipyRotationTime)
        {
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation,endRotationAmount, elapsedTime/flipRotationTime);
            playerTransform.rotation = Quaternion.Euler(0f,yRotation, 0f);
            yield return null;
        }

    }
    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;
        if (isFacingRight)
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }

}
