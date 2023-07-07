using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    private DialogueSystem _dialogueSystem;

    private Animator anim;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _dialogueSystem = gameObject.GetComponent<DialogueSystem>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!_dialogueSystem.InDialogue)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
        
            Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed * Time.deltaTime;
        
            transform.Translate(movement);
        }

        if (_dialogueSystem.InDialogue)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
