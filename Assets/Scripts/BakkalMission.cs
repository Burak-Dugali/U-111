using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakkalMission : MonoBehaviour
{

    [SerializeField] private List<GameObject> Apples;

    private bool _canBeTakeApples;
    private int _applesCounter;

    private DialogueSystem _dialogueSystem;
    void Start()
    {
        _dialogueSystem = GameObject.Find("Player").GetComponent<DialogueSystem>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canBeTakeApples)
        {
            Apples[_applesCounter].SetActive(false);
            Apples.RemoveAt(_applesCounter);

            if (Apples.Count == 0)
            {
                _canBeTakeApples = false;
                _dialogueSystem.BakkalMissionComplete = true;
                _dialogueSystem.RunTrriggerStay();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _canBeTakeApples = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canBeTakeApples = false;
        }
    }
}
