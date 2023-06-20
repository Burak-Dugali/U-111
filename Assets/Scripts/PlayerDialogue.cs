using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI NPCName;
    public bool InDalogue;
    void Start()
    {
        DialoguePanel.SetActive(false);
        InDalogue = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InDalogue = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("NPC") && InDalogue == false)
        {
            NPCName.text = other.name;
            DialoguePanel.SetActive(true);
            InDalogue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            DialoguePanel.SetActive(false);
        }
    }
}
