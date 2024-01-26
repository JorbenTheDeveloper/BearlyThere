using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private DialogueCrow _dialogueCrow;
    //[SerializeField] private DialogueCrow dialogueCrow;
    [SerializeField] int crowID;


    private void Awake()
    {
        _dialogueCrow = dialogueBox.GetComponent<DialogueCrow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCharacter"))
        {
            dialogueBox.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
