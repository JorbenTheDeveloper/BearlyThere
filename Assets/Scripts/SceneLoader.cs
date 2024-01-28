using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Animator animator; // Reference to the Animator
    public float animationDuration = 2f; // Duration of the animation

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCharacter"))
        {
            StartCoroutine(PlayAnimationAndSwitchScene());
        }
    }

    IEnumerator PlayAnimationAndSwitchScene()
    {
        if (animator != null)
        {
            // Trigger the animation
            animator.SetTrigger("StartAnimation"); // Replace with your trigger parameter

            // Wait for the animation to finish
            yield return new WaitForSeconds(animationDuration);
        }

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(PlayAnimationAndSwitchScene(sceneName));
    }

    IEnumerator PlayAnimationAndSwitchScene(string sceneName)
    {
        if (animator != null)
        {
            // Trigger the animation
            animator.SetTrigger("StartAnimation"); // Replace with your trigger parameter

            // Wait for the animation to finish
            yield return new WaitForSeconds(animationDuration);
        }

        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }

}
