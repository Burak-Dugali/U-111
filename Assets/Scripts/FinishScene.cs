using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class FinishScene : MonoBehaviour
{
    [SerializeField] private Image panelImage; // Panelin Image bileşeni
    
    public void StartFinishScene()
    {
        gameObject.SetActive(true);
        panelImage.DOFade(255, 500f);
    }

    private void Start()
    {

    }
}
