using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public GameObject DialoguePanel;

    [SerializeField] private TextMeshProUGUI _currentText;
    
    public bool InDialogue;
    public bool InNPCDialogue;
    private bool _canbeTalk;
    private int _dialougeCounter;
    
    private Collider2D collider;

    private int _chooseRandomText;
    
    [SerializeField] private DialogueCreator _npcDialogue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canbeTalk)
        {
            InDialogue = true;
            InNPCDialogue = true;
            _chooseRandomText = Random.Range(1, 6);
            _currentText.text = _npcDialogue.Texts[_chooseRandomText];
            _canbeTalk = false;
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
        }

        if (InDialogue == false)
        {
            DialoguePanel.SetActive(false);
        }
        else
        {
            DialoguePanel.SetActive(true);
        }

        Debug.Log(InNPCDialogue);
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
        if (other.CompareTag("NPC") && InDialogue && InNPCDialogue == false)
        {
            DialoguePanel.SetActive(true);
            InNPCDialogue = true;
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
