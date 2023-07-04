using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueSystem : MonoBehaviour
{
    public GameObject DialoguePanel;

    [SerializeField] private TextMeshProUGUI _currentText;
    [SerializeField] private TextMeshProUGUI _npcName;

    public GameObject AlkolButton1;
    public GameObject AlkolButton2;

    private int _alkolCounter;

    public bool InDialogue;
    
    private bool InNPCDialogue;
    private bool _canbeTalk;
    private bool _entryTextCompleted = false;
    private int _dialougeCounter = 0;
    
    private Collider2D collider;

    private int _chooseRandomText;
    
    [SerializeField] private DialogueCreator _npcDialogue;
    [SerializeField] private DialogueCreator _objectiveNpcDialogue;
    [SerializeField] private Image _npcSprite;

    [Header("Sprites")] 
    [SerializeField] private Sprite Bakkal;
    [SerializeField] private Sprite Berber;
    [SerializeField] private Sprite Genc;
    [SerializeField] private Sprite Hanci;
    [SerializeField] private Sprite Kahraman;
    [SerializeField] private Sprite Kizimiz;
    [SerializeField] private Sprite Postaci;
    [SerializeField] private Sprite Tatlici;
    [SerializeField] private Sprite Terzi;
    [SerializeField] private Sprite Ciftci;
    [SerializeField] private Sprite Cicekci;
    [SerializeField] private Sprite Coban;
    [SerializeField] private Sprite Ogretmen;
    
    [Header("GameObjects")]
    [SerializeField] private GameObject BakkalGo;
    [SerializeField] private GameObject BerberGo;
    [SerializeField] private GameObject GencGo;
    [SerializeField] private GameObject HanciGo;
    [SerializeField] private GameObject KahramanGo;
    [SerializeField] private GameObject KizimizGo;
    [SerializeField] private GameObject PostaciGo;
    [SerializeField] private GameObject TatliciGo;
    [SerializeField] private GameObject TerziGo;
    [SerializeField] private GameObject CiftciGo;
    [SerializeField] private GameObject CicekciGo;
    [SerializeField] private GameObject CobanGo;
    [SerializeField] private GameObject OgretmenGo;
    

    void Start()
    {
        InDialogue = true;
        FirstText();
        collider = GameObject.Find("Hancı").GetComponent<Collider2D>();
    }

    void Update()
    {
        //Debug.Log(InDialogue + "  In dialogue");
        //Debug.Log(InNPCDialogue + "  In NPC dialogue");
        //Debug.Log(_canbeTalk + "  Can be talk");
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
        if (other.CompareTag("NPC") && InDialogue && InNPCDialogue)
        {
            InDialogue = true;
            InNPCDialogue = true;
            switch (other.gameObject.name)
            {
                case "Hancı":
                    _npcSprite.sprite = Hanci;
                    break;
                case "Bakkal":
                    _npcSprite.sprite = Bakkal;
                    break;
                case "Çiftçi":
                    _npcSprite.sprite = Ciftci;
                    break;
            }
        }

        if (other.CompareTag("ObjectiveNPC") && InDialogue)
        {
            //Debug.Log("Objective trigger calisti");
            _currentText.text = _objectiveNpcDialogue.Texts[_dialougeCounter];
            switch (_dialougeCounter)
            {
                //Kahramanımız
                case 0:
                case 1:
                case 4:
                case 7:
                case 9:
                    _npcSprite.sprite = Kahraman;
                    break;
                case 13:    
                    _npcSprite.sprite = Kahraman;
                    AlkolButton1.SetActive(true);
                    AlkolButton2.SetActive(true);
                    break;
                //Hancı
                case 3:  
                case 5:
                    _npcSprite.sprite = Hanci;
                    break;
                case 11:
                    InDialogue = false;
                    _npcSprite.sprite = Hanci;
                    HanciGo.tag = "ObjectiveNPC";
                    CiftciGo.tag = "NPC";
                    break;
                case 12:    
                    _npcSprite.sprite = Hanci;
                    break;
                //Çiftçi
                case 6:
                    InDialogue = false;
                    HanciGo.tag = "NPC";
                    CiftciGo.tag = "ObjectiveNPC";
                    _npcSprite.sprite = Ciftci;
                    break;
                case 8:
                    _npcSprite.sprite = Ciftci;
                    break;
                case 10:
                    InDialogue = false;
                    _npcSprite.sprite = Ciftci;
                    return;
            }
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
        _npcSprite.sprite = Kahraman;
        _currentText.text = _objectiveNpcDialogue.Texts[_dialougeCounter];
    }

    public void ChooseDrink()
    {
        _alkolCounter++;
        _dialougeCounter++;
    }

    public void ChooseDontDrink()
    {
        _dialougeCounter++;
    }
}
