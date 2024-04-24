using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pilot : Subject
{
    private bool isAsleep = false;
    private Coroutine DelaySleepCoroutine;
    private Coroutine SleepingCoroutine;

    public void StartSleepDelay()
    {
        float delayDuration = Random.Range(30f, 45f);
        if (DelaySleepCoroutine != null)
        {
            StopCoroutine(DelaySleepCoroutine);
        }
        DelaySleepCoroutine = StartCoroutine(DelaySleep(delayDuration));
    }

    private IEnumerator DelaySleep(float delay)
    {
        Debug.Log("Delay");
        yield return new WaitForSeconds(delay);
        StartSleeping();
    }

    private void StartSleeping()
    {
        isAsleep = true;
        if (SleepingCoroutine != null)
        {
            StopCoroutine(SleepingCoroutine);
        }
        SleepingCoroutine = StartCoroutine(Sleep());
        Debug.Log("Pilot has started sleeping");
    }

    private IEnumerator Sleep()
    {

        while (isAsleep)
        {
            Debug.Log("Disminuir puntos");
            Notify(Actions.RemovePoints);
            yield return new WaitForSeconds(1);
        }
    }

    public void WakeUp()
    {
        if (isAsleep)
        {
            isAsleep = false;
            if (DelaySleepCoroutine != null)
            {
                StopCoroutine(SleepingCoroutine);
                StartSleepDelay();
            }
            Debug.Log("Pilot has been awakened");
        }
        else
        {
            Debug.Log("The pilot is not asleep");
        }
    }
}
