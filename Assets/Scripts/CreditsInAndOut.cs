using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CreditsInAndOut : MonoBehaviour
{
    // Function to change scene to "StartScene"
    public PlayableDirector toStartDirector;
    public PlayableDirector toCreditsDirector;
    public void ChangeToStartScene()
    {
        if (toStartDirector != null)
        {
            toStartDirector.Play();
            StartCoroutine(WaitForTimeline(toStartDirector, "Start"));
        }
        else
        {
            SceneManager.LoadScene("Start");
        }
    }

    // Function to change scene to "Credits"
    public void ChangeToCreditsScene()
    {
        if (toCreditsDirector != null)
        {
            toCreditsDirector.Play();
            StartCoroutine(WaitForTimeline(toCreditsDirector, "Credits"));
        }
        else
        {
            SceneManager.LoadScene("Credits");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitForTimeline(PlayableDirector director, string sceneName)
    {
        yield return new WaitUntil(() => director.state != PlayState.Playing);
        SceneManager.LoadScene(sceneName);
    }
}
