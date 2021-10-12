using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { private set; get; }

    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Text _tempsRestantText;
    [SerializeField] private Text _timesUpText;
    private float _tempsJeu = 300f;
    public float _tempsRestant;
    public bool _estArrete;
    private string _textTime;
    private float stopTime;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        _timesUpText.gameObject.SetActive(false);
        _estArrete = false;
        _timeSlider.maxValue = _tempsJeu;
        _timeSlider.value = _tempsJeu;
    }

    // Update is called once per frame
    void Update()
    {
        _tempsRestant = _tempsJeu - Time.time;
        int minutes = Mathf.FloorToInt(_tempsRestant / 60);
        int secondes = Mathf.FloorToInt(_tempsRestant - minutes * 60f);
        _textTime = string.Format("{0:00}:{1:00}", minutes, secondes);

        if (_estArrete == false)
        {
            _tempsRestantText.text = _textTime;
            _timeSlider.value = _tempsRestant;
        }

        if (_tempsRestant <= 0)
        {
            _estArrete = true;
            _timesUpText.gameObject.SetActive(true);
            Debug.Log("Le temps s'est terminé");
        }

    }
    private float StartTimer()
    {
        return _tempsRestant;
    }

    private void StopTimer()
    {
        Debug.Log("Time stop");
        _estArrete = false;
        
    }
}
