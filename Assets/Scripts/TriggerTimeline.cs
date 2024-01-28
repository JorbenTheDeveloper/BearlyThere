using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : MonoBehaviour
{
    public PlayableDirector timeline;
    public float delayInSeconds = 5f;

    void Start()
    {
        StartCoroutine(StartTimelineAfterDelay());
    }

    IEnumerator StartTimelineAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        timeline.Play();
    }
}
