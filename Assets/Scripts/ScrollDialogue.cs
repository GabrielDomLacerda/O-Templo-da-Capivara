using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollDialogue : MonoBehaviour
{
    
    public string[] dialogueScroll;
    public int dialogueIndex;
    public GameObject dialoguePanel;
    public Text dialogueText;

    public bool readyToSpeak;
    public bool startDialogue;


    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && readyToSpeak)
        {
            if (!startDialogue)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueScroll[dialogueIndex])
            {
                NextDialogue();
            }
            readyToSpeak = false;
        }
    }

    void NextDialogue()
    {
        dialogueIndex ++;

        if (dialogueIndex < dialogueScroll.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
        }
    }

    void StartDialogue()
    {
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        dialogueText.text = "";
        foreach (char letter in dialogueScroll[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        dialoguePanel.SetActive(false);
    }

    public void DialogueTrue()
    {
        readyToSpeak = true;
    }

    public void DialogueFalse()
    {
        readyToSpeak = false;
    }


}
