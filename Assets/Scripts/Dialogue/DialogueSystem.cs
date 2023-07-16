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

    private FinishScene _finishScene;
    public GameObject FinishScreen;

    public bool BakkalMissionComplete;
    public bool CiftciMissionComplete;
    
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
    [SerializeField] private Sprite Sarapci;
    [SerializeField] private Sprite Zarf;
    
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
    [SerializeField] private GameObject SarapciGo;
    
    [Header("Sign GameObjects")]
    [SerializeField] private GameObject BakkalSign;
    [SerializeField] private GameObject BerberSign;
    [SerializeField] private GameObject GencSign;
    [SerializeField] private GameObject HanciSign;
    [SerializeField] private GameObject KizimizSign;
    [SerializeField] private GameObject PostaciSign;
    [SerializeField] private GameObject TatliciSign;
    [SerializeField] private GameObject TerziSign;
    [SerializeField] private GameObject CiftciSign;
    [SerializeField] private GameObject CicekciSign;
    [SerializeField] private GameObject CobanSign;
    [SerializeField] private GameObject OgretmenSign;
    [SerializeField] private GameObject HomeSign;
    [SerializeField] private GameObject SarapciSign;

    private string BakkalName = "Bakkal";
    private string BerberName = "Berber";
    private string GencName = "Genç";
    private string HanciName = "Hancı";
    private string KahramanName = "Steve";
    private string KizimizName = "Nancy";
    private string PostaciName = "Postacı";
    private string TatliciName = "Tatlıcı";
    private string TerziName = "Terzi";
    private string CiftciName = "Çiftçi";
    private string CicekciName = "Çiçekçi";
    private string CobanName = "Çoban";
    private string OgretmenName = "Öğretmen";
    private string SarapciName = "Şarapçı";
    private string ZarfName = "Alice";
    
    [SerializeField] private GameObject _tutorial;
    
    void Start()
    {
        InDialogue = true;
        FirstText();
        collider = GameObject.Find("Hancı").GetComponent<Collider2D>();
        HanciSign.SetActive(true);
        _finishScene = FinishScreen.GetComponent<FinishScene>();
        PostaciGo.SetActive(false);
        
    }

    void Update()
    {
        //Debug.Log(_canbeTalk);
        //Debug.Log(InChoose);
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

        if (AlkolButton1.activeInHierarchy || YemekSecimi1.activeInHierarchy)
        {
            InChoose = true;
        }

        else
        {
            InChoose = false;
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
                case 57:
                case 64:    
                case 66:    
                case 68: 
                case 70: 
                case 73:
                case 75:    
                case 81:
                case 90:    
                case 97:
                case 101:
                case 103:
                case 105:
                case 108:
                case 111:
                case 113:
                case 114:
                case 116:
                case 121:
                case 124:
                case 126:
                case 128:
                case 130:
                case 134:
                case 138:
                case 142:
                case 94:
                    _npcSprite.sprite = Kahraman;
                    _npcName.text = KahramanName;
                    break;
                case 131:
                    _finishScene.StartFinishScene();
                    break;
                case 144:
                    _finishScene.StartFinishScene();
                    break;
                case 83:
                    _npcSprite.sprite = Kahraman;
                    _npcName.text = KahramanName;
                    PostaciGo.SetActive(true);
                    break;
                case 40:          
                    YemekSecimi1.SetActive(true);
                    YemekSecimi2.SetActive(true);
                    _npcSprite.sprite = Kahraman;
                    _npcName.text = KahramanName;
                    InChoose = true;
                    break;
                case 46:
                    AlkolButton1.SetActive(true);
                    AlkolButton2.SetActive(true);
                    InChoose = true;
                    ButtonText1.text = "Kravatı bulmak için enerji lazım! (Alkol İçer)";
                    ButtonText2.text = "Burayı temizleyip, bunlardan kurtulsam iyi olur! (Alkol içmez)";
                    _npcName.text = KahramanName;
                    break;
                case 48:
                    _npcSprite.sprite = Kahraman;
                    _npcName.text = KahramanName;
                    InDialogue = false;
                    HomeGo.tag = "Home";
                    TerziGo.tag = "ObjectiveNPC";
                    HomeSign.SetActive(false);
                    TerziSign.SetActive(true);
                    _dialougeCounter++;
                    break;
                case 61:
                    _npcSprite.sprite = Kahraman;
                    _npcName.text = KahramanName;
                    AlkolButton1.SetActive(true);
                    AlkolButton2.SetActive(true);
                    InChoose = true;
                    ButtonText1.text = "Hiç teklif etmeyeceksiniz zannettim! (Alkol içer)";
                    ButtonText2.text = "Öğretmeninize söylemeden atın onu! (Alkol içmez)";
                    break;
                case 62:
                    _dialougeCounter++;
                    InDialogue = false;
                    GencGo.tag = "NPC";
                    TerziGo.tag = "ObjectiveNPC";
                    GencSign.SetActive(false);
                    TerziSign.SetActive(true);
                    break;
                case 71:
                    InDialogue = false;
                    TerziGo.tag = "NPC";
                    BerberGo.tag = "ObjectiveNPC";
                    TerziSign.SetActive(false);
                    BerberSign.SetActive(true);
                    _dialougeCounter++;
                    break;
                case 76:
                    InDialogue = false;
                    BerberGo.tag = "NPC";
                    SarapciGo.tag = "ObjectiveNPC";
                    BerberSign.SetActive(false);
                    SarapciSign.SetActive(true);
                    _dialougeCounter++;
                    break;
                case 78:
                    _npcSprite.sprite = Kahraman;
                    _npcName.text = KahramanName;
                    AlkolButton1.SetActive(true);
                    AlkolButton2.SetActive(true);
                    InChoose = true;
                    ButtonText1.text = "HEY HEY HEY!!! ŞİMDİDEN KENDİME DAHA ÇOK GÜVENİYORUM. (Alkol içer)";
                    ButtonText2.text = "Hey sana afiyet olsun. Benim işlerim var. (Alkol içmez)";
                    break;
                case 79:
                    _dialougeCounter++;
                    InDialogue = false;
                    SarapciGo.tag = "NPC";
                    CicekciGo.tag = "ObjectiveNPC";
                    CicekciSign.SetActive(true);
                    SarapciSign.SetActive(false);
                    break;
                case 84:
                    _dialougeCounter++;
                    InDialogue = false;
                    CicekciGo.tag = "NPC";
                    PostaciGo.tag = "ObjectiveNPC";
                    PostaciSign.SetActive(true);
                    CicekciSign.SetActive(false);
                    break;
                case 91:
                    _dialougeCounter++;
                    InDialogue = false;
                    PostaciGo.tag = "NPC";
                    TatliciGo.tag = "ObjectiveNPC";
                    TatliciSign.SetActive(true);
                    PostaciSign.SetActive(false);
                    break;
                case 95:
                    InDialogue = false;
                    _dialougeCounter++;
                    TatliciGo.tag = "NPC";
                    KizimizGo.tag = "ObjectiveNPC";
                    TatliciSign.SetActive(false);
                    KizimizSign.SetActive(true);
                    if (_alkolCounter == 2 || _alkolCounter == 3)
                    {
                        _dialougeCounter = 120;
                    }

                    if (_alkolCounter == 4 || _alkolCounter > 4)
                    {
                        _dialougeCounter = 133;
                    }
                    break;
                case 15:    
                    _npcSprite.sprite = Kahraman;
                    _npcName.text = KahramanName;
                    AlkolButton1.SetActive(true);
                    AlkolButton2.SetActive(true);
                    InChoose = true;
                    break;
                case 10:
                    InDialogue = false;
                    _canbeTalk = false;
                    if (CiftciMissionComplete)
                    {
                        _dialougeCounter++;
                        InDialogue = true;
                        _canbeTalk = true;
                    }
                    break;
                //Hancı
                case 3:  
                case 5:
                    _npcSprite.sprite = Hanci;
                    _npcName.text = HanciName;
                    _tutorial.SetActive(false);
                    break;
                case 13:
                    _npcSprite.sprite = Hanci;
                    _npcName.text = HanciName;
                    break;
                case 14:
                    _npcSprite.sprite = Hanci;
                    _npcName.text = HanciName;
                    break;
                case 16:
                    _npcSprite.sprite = Hanci;
                    _npcName.text = HanciName;
                    break;
                case 17:
                    InDialogue = false;
                    InNPCDialogue = false;
                    _canbeTalk = false;
                    _canbeTalk = true;
                    _npcSprite.sprite = Hanci;
                    _npcName.text = HanciName;
                    HanciGo.tag = "NPC";
                    BakkalGo.tag = "ObjectiveNPC";
                    HanciSign.SetActive(false);
                    BakkalSign.SetActive(true);
                    _dialougeCounter++;
                    break;
                //Çiftçi
                case 6:
                    InDialogue = false;
                    HanciGo.tag = "NPC";
                    CiftciGo.tag = "ObjectiveNPC";
                    HanciSign.SetActive(false);
                    CiftciSign.SetActive(true);
                    _npcSprite.sprite = Ciftci;
                    _npcName.text = CiftciName;
                    _dialougeCounter++;
                    break;
                case 7:
                case 9:
                    _npcSprite.sprite = Ciftci;
                    _npcName.text = CiftciName;
                    break;
                case 11:
                    _npcSprite.sprite = Ciftci;
                    _npcName.text = CiftciName;
                    break;
                case 12:
                    InDialogue = false;
                    HanciGo.tag = "ObjectiveNPC";
                    CiftciGo.tag = "NPC";
                    HanciSign.SetActive(true);
                    CiftciSign.SetActive(false);
                    _dialougeCounter++;
                    break;
                case 33: 
                case 35:
                case 37:
                    _npcSprite.sprite = Ciftci;
                    _npcName.text = CiftciName;
                    break;
                case 38:
                    InDialogue = false;
                    CobanGo.tag = "ObjectiveNPC";
                    CiftciGo.tag = "NPC";
                    CobanSign.SetActive(true);
                    CiftciSign.SetActive(false);
                    _dialougeCounter++;
                    break;
                //Bakkal
                case 18:
                case 20:
                case 23:
                case 25:    
                    _npcSprite.sprite = Bakkal;
                    _npcName.text = BakkalName;
                    break;
                case 22:
                    InDialogue = false;
                    _canbeTalk = false;
                    if (BakkalMissionComplete)
                    {
                        _dialougeCounter++;
                        InDialogue = true;
                        _canbeTalk = true;
                    }
                    break;
                case 27:
                    InDialogue = false;
                    CobanGo.tag = "ObjectiveNPC";
                    BakkalGo.tag = "NPC";
                    CobanSign.SetActive(true);
                    BakkalSign.SetActive(false);
                    _dialougeCounter++;
                    break;
                //Çoban
                case 28:
                case 39:    
                case 30:
                case 41:
                case 43:    
                    _npcSprite.sprite = Coban;
                    _npcName.text = CobanName;
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
                    HomeSign.SetActive(true);
                    CobanSign.SetActive(false);
                    _dialougeCounter++;
                    break;
                case 32:
                    InDialogue = false;
                    CiftciGo.tag = "ObjectiveNPC";
                    CobanGo.tag = "NPC";
                    CobanSign.SetActive(false);
                    CiftciSign.SetActive(true);
                    _dialougeCounter++;
                    break;
                //Terzi
                case 63:
                case 65:
                case 67:
                case 69:    
                case 49:
                    _npcSprite.sprite = Terzi;
                    _npcName.text = TerziName;
                    break;
                case 51:
                    InDialogue = false;
                    OgretmenGo.tag = "ObjectiveNPC";
                    TerziGo.tag = "NPC";
                    OgretmenSign.SetActive(true);
                    TerziSign.SetActive(false);
                    _dialougeCounter++;
                    break;
                //Öğretmen
                case 52:
                case 54:  
                case 58:    
                case 56:
                    _npcSprite.sprite = Ogretmen;
                    _npcName.text = OgretmenName;
                    break;
                case 59:
                    InDialogue = false;
                    OgretmenGo.tag = "NPC";
                    GencGo.tag = "ObjectiveNPC";
                    OgretmenSign.SetActive(false);
                    GencSign.SetActive(true);
                    _dialougeCounter++;
                    break;
                //Genç
                case 60:
                    _npcSprite.sprite = Genc;
                    _npcName.text = GencName;
                    break;
                //Berber
                case 72:
                case 74:
                    _npcSprite.sprite = Berber;
                    _npcName.text = BerberName;
                    break;
                //Şarapçı
                case 77:
                    _npcSprite.sprite = Sarapci;
                    _npcName.text = SarapciName;
                    break;
                //Çiçekçi
                case 80:
                case 82:
                    _npcSprite.sprite = Cicekci;
                    _npcName.text = CicekciName;
                    break;
                //Postacı
                case 85:
                    _npcSprite.sprite = Postaci;
                    _npcName.text = PostaciName;
                    break;
                //Mektup
                case 86:
                case 87:
                case 88:
                case 89:
                    _npcSprite.sprite = Zarf;
                    _npcName.text = ZarfName;
                    break;
                //Tatlıcı
                case 92:
                    _npcSprite.sprite = Tatlici;
                    _npcName.text = TatliciName;
                    break;
                //Kızımız
                case 96:
                case 98:
                case 99:
                case 100:
                case 102:
                case 104:
                case 106:
                case 107:
                case 109:
                case 110:
                case 112:
                case 115:
                case 117:
                case 120:
                case 122:
                case 123:
                case 125:
                case 127:
                case 129:
                case 133:
                case 135:
                case 136:
                case 137:
                case 139:
                case 141:
                case 143:
                    _npcSprite.sprite = Kizimiz;
                    _npcName.text = KizimizName;
                    break;
                case 118:
                    _finishScene.StartFinishScene();
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
        InChoose = false;
    }

    public void ChooseDontDrink()
    {
        NextText();
        AlkolButton1.SetActive(false);
        AlkolButton2.SetActive(false);
        InChoose = false;
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
        CobanSign.SetActive(false);
        CiftciSign.SetActive(true);
        YemekSecimi1.SetActive(false);
        YemekSecimi2.SetActive(false);
        InChoose = false;
        NextText();
    }
    
}
