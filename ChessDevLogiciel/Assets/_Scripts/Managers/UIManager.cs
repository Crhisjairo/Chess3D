using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Text timerText;
    [SerializeField] private Image _couleur;
    private float _tempsJeu = 20;
    public float _decompte;
    private bool stopTimer;


    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timeSlider.maxValue = _tempsJeu;
        timeSlider.value = _tempsJeu;
    }

    // Update is called once per frame
    void Update()
    {
        tempsEcoule();
       
    }

    public void tempsEcoule()
    {
        _decompte = _tempsJeu - Time.time;

        int minutes = Mathf.FloorToInt(_decompte / 60);
        int secondes = Mathf.FloorToInt(_decompte - minutes * 60f);

        string textTime = string.Format("{0:00}:{1:00}", minutes, secondes);

        if (_decompte <= 6)
        {
            _couleur.color = Color.red;

            if (_decompte <= 0)
            {
                stopTimer = true;
            }
        }

        if (stopTimer == false)
        {
            timerText.text = textTime;
            timeSlider.value = _decompte;
        }
    }
}
