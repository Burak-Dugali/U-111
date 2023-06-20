using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI nameText;

    private DialogueContainer currentDialogue;
    private int currentTextLine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
    }

    private void PushText()
    {
        currentTextLine += 1;
        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        }
        else
        {
            targetText.text = currentDialogue.line[currentTextLine];
        }
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {
        currentDialogue = dialogueContainer;
        currentTextLine = 0;
        targetText.text = currentDialogue.line[currentTextLine];
    }

    private void Conclude()
    {
        Debug.Log("Dilougue ended");
    }
}
