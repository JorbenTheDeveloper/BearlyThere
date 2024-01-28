using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInController : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string sceneToLoad; // Name of the scene to load after fade-in
    public float delay = 2f; // Delay in seconds

    public void FadeIn()
    {
        // Trigger the fade-in animation
        animator.SetBool("StartFadeIn", true);

        // Start the coroutine to load the scene after a delay
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
