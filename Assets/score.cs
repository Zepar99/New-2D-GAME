using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class score : MonoBehaviour
{
   private TextMeshProUGUI scoreText;
   private int Score = 0;
   private void Awake()
   {
       scoreText = GetComponent<TextMeshProUGUI>();
   }
    private void Start()
    {
        RefreshUI();
    }
    public void increaseScore(int increment)
    {
        Score += increment;
        RefreshUI();
    }
    private void RefreshUI()
    {
        scoreText.text = "Score:"+ Score;
    }
}
