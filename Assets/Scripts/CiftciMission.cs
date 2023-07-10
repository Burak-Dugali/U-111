using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CiftciMission : MonoBehaviour
{
    public List<GameObject> Otlar;

    private DialogueSystem _dialogueSystem;
    private Collider2D col2d;
    private int otCounter;
    private GameObject Ot;
    void Start()
    {
        _dialogueSystem = GetComponent<DialogueSystem>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (col2d != null)
            {
            col2d.GameObject().SetActive(false);
            Otlar.Remove(Ot);
            otCounter++;
            }
        }

        if (Otlar.Count == 0)
        {
            _dialogueSystem.CiftciMissionComplete = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Otlar"))
        {
            col2d = col;
            Ot = col.gameObject;
        }
    }
}
