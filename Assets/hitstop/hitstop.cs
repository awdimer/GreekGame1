using UnityEngine;
using System.Collections;

public class Hitstop : MonoBehaviour
{
    bool waiting;
    public void Stop(float durration)
    {
        if (waiting)
        {
            return;
        }
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(durration));

    }
    IEnumerator Wait(float durration)
    {
        yield return new WaitForSecondsRealtime(durration);
        Time.timeScale = 1.0f;
    }
}
