using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { private set; get; }

    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _timesUpText;
    [SerializeField] private Image _couleur;
    private float _tempsJeu = 300f;
    public float _decompte;
    private bool _stopTimer;


    // Start is called before the first frame update
    void Start()
    {
        _timesUpText.gameObject.SetActive(false);
        _stopTimer = false;
        _timeSlider.maxValue = _tempsJeu;
        _timeSlider.value = _tempsJeu;
    }

    // Update is called once per frame
    void Update()
    {
        tempsEcoule();
       
    }

    public float tempsEcoule()
    {
        _decompte = _tempsJeu - Time.time;
        int minutes = Mathf.FloorToInt(_decompte / 60);
        int secondes = Mathf.FloorToInt(_decompte - minutes * 60f);
        string textTime = string.Format("{0:00}:{1:00}", minutes, secondes);

        
            if (_decompte <= 6)
            {
                _couleur.color = Color.red;
                _timerText.color = Color.red;
            }
            else if (_decompte <= 0)
            {
                _stopTimer = true;
                _timesUpText.gameObject.SetActive(true);
                Debug.Log("Le temps s'est arreté");
            }
        

        if (_stopTimer == false)
        {
            _timerText.text = textTime;
            _timeSlider.value = _decompte;
        }
        return _decompte;
    }
}
