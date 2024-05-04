using System.Collections;
using UnityEngine;

public class Pilot : Subject
{
    [SerializeField] private GameObject sleepingImage;
    [SerializeField] private AudioSource pilotAudioSource;
    bool isAsleep = false;
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
        pilotAudioSource.Play();
        SleepingCoroutine = StartCoroutine(Sleep());
        sleepingImage.SetActive(true);
        Debug.Log("Pilot has started sleeping");
    }

    private IEnumerator Sleep()
    {
        while (isAsleep)
        {
            Notify(Actions.RemovePoints);
            yield return new WaitForSeconds(2);
            ScoreManager.instance.DeductPoints(5);
        }
    }

    public void WakeUp()
    {
        if (isAsleep)
        {
            isAsleep = false;
            if (DelaySleepCoroutine != null)
            {
                pilotAudioSource.Stop();
                StopCoroutine(SleepingCoroutine);
                StartSleepDelay();
            }
            sleepingImage.SetActive(false);
            Debug.Log("Pilot has been awakened");
            
        }
        else
        {
            Debug.Log("The pilot is not asleep");
           
        }
    }


    public bool IsAsleep (){
        return isAsleep;
    }
}
