using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad;
    public PlayableDirector timelineDirector;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerCharacter"))
        {
            // Start the timeline
            timelineDirector.Play();

            // Start coroutine to wait for the timeline to finish before loading the scene
            StartCoroutine(WaitForTimelineToFinish());
        }
    }

    IEnumerator WaitForTimelineToFinish()
    {
        // Wait for the timeline to finish
        yield return new WaitForSeconds((float)timelineDirector.duration);

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
