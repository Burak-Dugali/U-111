using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private DialogueSystem _dialogueSystem;

    private void Awake()
    {
        _dialogueSystem = gameObject.GetComponent<DialogueSystem>();
    }

    void Update()
    {
        if (!_dialogueSystem.InDialogue)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
        
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;
        
            transform.Translate(movement);
        }
    }
}
