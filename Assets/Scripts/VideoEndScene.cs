using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public PlayableDirector timeline;
    public string sceneToLoad;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        timeline.Play();
        StartCoroutine(WaitForTimelineToEnd());
    }

    IEnumerator WaitForTimelineToEnd()
    {
        // Wait for the duration of the timeline
        yield return new WaitForSeconds((float)timeline.duration);

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
