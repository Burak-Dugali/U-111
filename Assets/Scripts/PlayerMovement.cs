using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private PlayerDialogue _playerDialogue;

    private void Awake()
    {
        _playerDialogue = gameObject.GetComponent<PlayerDialogue>();
    }

    void Update()
    {
        if (!_playerDialogue.InDialogue)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
        
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;
        
            transform.Translate(movement);
        }
    }
}
