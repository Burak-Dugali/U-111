using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogue : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI NPCName;
    
    [SerializeField] private Image NPCSprite;
    [SerializeField] private TextMeshProUGUI _currentText;
    
    public bool InDialogue;
    private bool _canbeTalk;
    private int _dialougeCounter;

    private Collider2D collider;
    private bool _kevinTakeFirstMission;
    
    [Header("SO")]
    [SerializeField] private DialogueCreator _kevinDialogue;
    [SerializeField] private DialogueCreator _playerDialogue;
    void Start()
    {
        DialoguePanel.SetActive(false);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && _canbeTalk)
        {
            InDialogue = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && InDialogue)
        {
            _dialougeCounter++;
            OnTriggerStay2D(collider);
            if (_kevinTakeFirstMission)
            {
                InDialogue = false;
            }
        }

        if (InDialogue == false)
        {
            DialoguePanel.SetActive(false);
        }
        else
        {
            DialoguePanel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("NPC") && InDialogue == false)
        {
            _canbeTalk = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("NPC") && InDialogue)
        {
            collider = other;
            NPCName.text = other.name;
            DialoguePanel.SetActive(true);
            if (other.name == "NPCKevin")
            {
                switch (_dialougeCounter)
                {
                    case 0:
                    case 2:
                    case 4:
                        break;
                    default:
                        break;
                }

                if (_kevinTakeFirstMission)
                {
                    _currentText.text = "Malzemeleri hala getirmedin mi!";
                }
                else
                {
                    if (_dialougeCounter < _kevinDialogue.Texts.Capacity)
                    {
                        _currentText.text = _kevinDialogue.Texts[_dialougeCounter];
                    }
                    else
                    {
                        _kevinTakeFirstMission = true;
                        InDialogue = false;
                        _currentText.text = "Malzemeleri hala getirmedin mi!";
                    }
                }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _canbeTalk = false;
            InDialogue = false;
        }
    }
}
