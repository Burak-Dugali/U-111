using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builds : MonoBehaviour
{
    public GameObject HanDis;
    public GameObject HanIc;
    public GameObject EvIc;
    public GameObject EvDis;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("HanDis"))
        {
            HanIc.SetActive(true);
        }

        if (col.CompareTag("EvDis"))
        {
            EvIc.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HanDis"))
        {
            HanIc.SetActive(false);
        }

        if (other.CompareTag("EvDis"))
        {
            EvIc.SetActive(false);
        }
    }
}
