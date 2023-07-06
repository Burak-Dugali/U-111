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
    public TextMeshProUGUI ButtonText1;
    public TextMeshProUGUI ButtonText2;
    public GameObject YemekSecimi1;
    public GameObject YemekSecimi2;

    private int _alkolCounter;

    public bool InDialogue;
    
    private bool InNPCDialogue;
    private bool _canbeTalk;
    private bool _entryTextCompleted;
    private bool InChoose;
    private int _dialougeCounter;
    
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
    [SerializeField] private GameObject HomeGo;
    

    void Start()
    {
        InDialogue = true;
        FirstText();
        collider = GameObject.Find("Hancı").GetComponent<Collider2D>();
    }

    void Update()
    {
        //Debug.Log(_canbeTalk);
        //Debug.Log(_dialougeCounter);
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
        
        if (Input.GetKeyDown(KeyCode.Space) && InDialogue && !InChoose)
        {
            NextText();
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

    private void NextText()
    {
        _dialougeCounter++;
        if (!_entryTextCompleted)
        {
            if (_dialougeCounter == 1)
            {
                FirstText();
                _entryTextCompleted = true;
                _canbeTalk = false;
            }
        }
        else
        {
            OnTriggerStay2D(collider);
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
                case "Çoban":
                    _npcSprite.sprite = Coban;
                    break;
            }
        }

        if ((other.CompareTag("ObjectiveNPC") && InDialogue))
        {
            _currentText.text = _objectiveNpcDialogue.Texts[_dialougeCounter];
            switch (_dialougeCounter)
            {
                //Kahramanımız
                case 0:
                case 1:
                case 4:
                case 8:
                case 19:
                case 21:   
                case 24:
                case 26:
                case 29:
                case 31:
                case 34:
                case 36:  
                case 44:    
                case 50:    
                case 53:
                case 55:    
                case 10:
                    _npcSprite.sprite = Kahraman;
                    break;
                case 40:   
                    YemekSecimi1.SetActive(true);
                    YemekSecimi2.SetActive(true);
                    _npcSprite.sprite = Kahraman;
                    InChoose = true;
                    break;
                case 46:
                    AlkolButton1.SetActive(true);
                    AlkolButton2.SetActive(true);
                    ButtonText1.text = "Kravatı bulmak için enerji lazım! (Alkol İçer)";
                    ButtonText2.text = "Burayı temizleyip, bunlardan kurtulsam iyi olur! (Alkol içmez)";
                    break;
                case 48:
                    _npcSprite.sprite = Kahraman;
                    InDialogue = false;
                    HomeGo.tag = "Home";
                    TerziGo.tag = "ObjectiveNPC";
                    _dialougeCounter++;
                    break;
                case 15:    
                    _npcSprite.sprite = Kahraman;
                    AlkolButton1.SetActive(true);
                    AlkolButton2.SetActive(true);
                    InChoose = true;
                    break;
                //Hancı
                case 3:  
                case 5:
                    _npcSprite.sprite = Hanci;
                    break;
                case 13:
                    _npcSprite.sprite = Hanci;
                    break;
                case 14:
                    _npcSprite.sprite = Hanci;
                    break;
                case 16:
                    _npcSprite.sprite = Hanci;
                    InChoose = false;
                    break;
                case 17:
                    InDialogue = false;
                    InNPCDialogue = false;
                    _canbeTalk = false;
                    _canbeTalk = true;
                    _npcSprite.sprite = Hanci;
                    HanciGo.tag = "NPC";
                    BakkalGo.tag = "ObjectiveNPC";
                    _dialougeCounter++;
                    break;
                //Çiftçi
                case 6:
                    InDialogue = false;
                    HanciGo.tag = "NPC";
                    CiftciGo.tag = "ObjectiveNPC";
                    _npcSprite.sprite = Ciftci;
                    _dialougeCounter++;
                    break;
                case 7:
                case 9:
                    _npcSprite.sprite = Ciftci;
                    break;
                case 11:
                    InDialogue = false;
                    _npcSprite.sprite = Ciftci;
                    break;
                case 12:
                    InDialogue = false;
                    HanciGo.tag = "ObjectiveNPC";
                    CiftciGo.tag = "NPC";
                    _dialougeCounter++;
                    break;
                case 33: 
                case 35:
                case 37:
                    _npcSprite.sprite = Ciftci;
                    break;
                case 38:
                    InDialogue = false;
                    CobanGo.tag = "ObjectiveNPC";
                    CiftciGo.tag = "NPC";
                    _dialougeCounter++;
                    break;
                //Bakkal
                case 18:
                case 20:
                case 23:
                case 25:    
                    _npcSprite.sprite = Bakkal;
                    break;
                case 22:
                    InDialogue = false;
                    _dialougeCounter++;
                    break;
                case 27:
                    InDialogue = false;
                    CobanGo.tag = "ObjectiveNPC";
                    BakkalGo.tag = "NPC";
                    _dialougeCounter++;
                    break;
                //Çoban
                case 28:
                case 39:    
                case 30:
                case 41:
                case 43:    
                    _npcSprite.sprite = Coban;
                    break;
                case 42:
                    InDialogue = false;
                    _npcSprite.sprite = Coban;
                    _dialougeCounter = 37;
                    CobanGo.tag = "NPC";
                    CiftciGo.tag = "ObjectiveNPC";
                    break;
                case 45:
                    InDialogue = false;
                    CobanGo.tag = "NPC";
                    HomeGo.tag = "ObjectiveNPC";
                    _dialougeCounter++;
                    break;
                case 32:
                    InDialogue = false;
                    CiftciGo.tag = "ObjectiveNPC";
                    CobanGo.tag = "NPC";
                    _dialougeCounter++;
                    break;
                //Terzi
                case 49:
                    _npcSprite.sprite = Terzi;
                    break;
                case 51:
                    InDialogue = false;
                    OgretmenGo.tag = "ObjectiveNPC";
                    TerziGo.tag = "NPC";
                    _dialougeCounter++;
                    break;
                //Öğretmen
                case 52:
                case 54:    
                case 56:
                    _npcSprite.sprite = Ogretmen;
                    break;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _canbeTalk = false;
            InDialogue = false;
            InNPCDialogue = false;
        }

        if (other.CompareTag("ObjectiveNPC"))
        {
            InNPCDialogue = false;
            _canbeTalk = false;
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
        NextText();
        AlkolButton1.SetActive(false);
        AlkolButton2.SetActive(false);
    }

    public void ChooseDontDrink()
    {
        NextText();
        AlkolButton1.SetActive(false);
        AlkolButton2.SetActive(false);
    }

    public void ChooseRightFood()
    {
        _dialougeCounter = 42;
        NextText();
        InChoose = false;
        YemekSecimi1.SetActive(false);
        YemekSecimi2.SetActive(false);
    }

    public void ChooseWrongFood()
    {
        Debug.Log("Calisti");
        YemekSecimi1.SetActive(false);
        YemekSecimi2.SetActive(false);
        InChoose = false;
        NextText();
    }
}
