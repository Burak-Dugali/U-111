using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueSystem : MonoBehaviour
{
    public GameObject DialoguePanel;

    [SerializeField] private TextMeshProUGUI _currentText;
    [SerializeField] private TextMeshProUGUI _npcName;
    
    public bool InDialogue;
    public bool InNPCDialogue;
    private bool _canbeTalk;
    private bool _entryTextCompleted = false;
    private int _dialougeCounter = 0;
    
    private Collider2D collider;

    private int _chooseRandomText;
    
    [SerializeField] private DialogueCreator _npcDialogue;
    [SerializeField] private DialogueCreator _objectiveNpcDialogue;

    void Start()
    {
        InDialogue = true;
        FirstText();
        collider = GameObject.Find("Hancı").GetComponent<Collider2D>();
    }

    void Update()
    {
        Debug.Log(InDialogue + "  In dialogue");
        Debug.Log(InNPCDialogue + "  In NPC dialogue");
        Debug.Log(_canbeTalk + "  Can be talk");
        if (Input.GetKeyDown(KeyCode.E) && _canbeTalk && collider.gameObject.CompareTag("NPC"))
        {
            _chooseRandomText = Random.Range(1, 6);
            _currentText.text = _npcDialogue.Texts[_chooseRandomText];
            _npcName.text = collider.gameObject.name;

            InDialogue = true;
            InNPCDialogue = true;
            _canbeTalk = false;
        }

        else if (Input.GetKeyDown(KeyCode.E) && _canbeTalk)
        {
            InDialogue = true;
            //InNPCDialogue = true;
            _canbeTalk = true;
        }

        if (_dialougeCounter == 2)
        {
            InDialogue = false;
            _dialougeCounter++;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && InNPCDialogue)
        {
            InDialogue = false;
            InNPCDialogue = false;
            _canbeTalk = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && InDialogue)
        {
            _dialougeCounter++;
            if (!_entryTextCompleted)
            {
                if (_dialougeCounter == 1)
                {
                    FirstText();
                    _entryTextCompleted = true;
                }
            }
            else
            {
                OnTriggerStay2D(collider);
            }
        }

        if (InDialogue == false && InNPCDialogue == false)
        {
            DialoguePanel.SetActive(false);
        }
        else
        {
            DialoguePanel.SetActive(true);
        }

        if (DialoguePanel.activeInHierarchy)
        {
            InDialogue = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("NPC") || (col.CompareTag("ObjectiveNPC") && InDialogue == false))
        {
            _canbeTalk = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        collider = other;
        if (other.CompareTag("NPC") && InDialogue && InNPCDialogue == false)
        {
            InDialogue = true;
            InNPCDialogue = true;
        }

        if (other.CompareTag("ObjectiveNPC") && InDialogue)
        {
            Debug.Log("Objective trigger calisti");
            _currentText.text = _objectiveNpcDialogue.Texts[_dialougeCounter];
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _canbeTalk = true;
            InDialogue = false;
            InNPCDialogue = false;
        }

        if (other.CompareTag("ObjectiveNPC"))
        {
            InNPCDialogue = false;
            _canbeTalk = true;
            InDialogue = false;
        }
    }

    private void FirstText()
    {
        _currentText.text = _objectiveNpcDialogue.Texts[_dialougeCounter];
    }
}
