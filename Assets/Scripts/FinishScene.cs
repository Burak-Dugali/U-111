using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class FinishScene : MonoBehaviour
{
    [SerializeField] private Image panelImage;  // Panelin Image bileşeni
    [SerializeField] private GameObject FinishText;  // Panelin Image bileşeni
    
    public void StartFinishScene()
    {
        gameObject.SetActive(true);
        panelImage.DOFade(255, 500f);
        Invoke("OpenFinishScene" ,3);
    }


    private void OpenFinishScene()
    {
        FinishText.SetActive(true);
    }
}
