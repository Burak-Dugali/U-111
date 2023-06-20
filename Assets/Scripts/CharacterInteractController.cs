using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    private PlayerMovement character;
    private Rigidbody2D rgbd2d;
    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;

    private void Awake()
    {
        character = GetComponent<PlayerMovement>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Vector2 position = rgbd2d.position + character.lastMotionVector
    }
}
