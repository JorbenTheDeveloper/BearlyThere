using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsInAndOut : MonoBehaviour
{
    // Function to change scene to "StartScene"
    public void ChangeToStartScene()
    {
        SceneManager.LoadScene("Start"); // Replace "StartScene" with your actual scene name
    }

    // Function to change scene to "Credits"
    public void ChangeToCreditsScene()
    {
        SceneManager.LoadScene("Credits"); // Replace "Credits" with your actual scene name
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
