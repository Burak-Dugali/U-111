using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<string> Text; 
    public static event Action<string> Name; 
    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
