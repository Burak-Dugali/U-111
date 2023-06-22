using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogue : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI NPCName;
    
    [SerializeField] private Image NPCSprite;
    [SerializeField] private TextMeshProUGUI _currentText;
    
    private bool InDialogue;
    private int _dialougeCounter;

    private Collider2D collider;

    [Header("SO")]
    [SerializeField] private DialogueCreator _kevinDialogue;
    [SerializeField] private DialogueCreator _playerDialogue;
    void Start()
    {
        DialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InDialogue = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && InDialogue)
        {
            _dialougeCounter++;
            OnTriggerStay2D(collider);
        }

        if (InDialogue == false)
        {
            DialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("NPC") && InDialogue)
        {
            collider = other;
            NPCName.text = other.name;
            DialoguePanel.SetActive(true);
            Debug.Log(_dialougeCounter);
            if (other.name == "NPCKevin")
            {
                switch (_dialougeCounter)
                {
                    case 0:
                    case 2:
                    case 4:
                        NPCSprite.sprite = _playerDialogue.NPCAsset;
                        break;
                    default:
                        NPCSprite.sprite = _kevinDialogue.NPCAsset;
                        break;
                }

                if (_dialougeCounter < _kevinDialogue.Texts.Capacity)
                {
                    _currentText.text = _kevinDialogue.Texts[_dialougeCounter];
                }
                else
                {
                    InDialogue = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            DialoguePanel.SetActive(false);
            InDialogue = false;
        }
    }
}
