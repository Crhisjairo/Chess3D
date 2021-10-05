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
        float time = _tempsJeu - Time.time;

        int minutes = Mathf.FloorToInt(time / 60);
        int secondes = Mathf.FloorToInt(time - minutes * 60f);

        string textTime = string.Format("{0:00}:{1:00}", minutes, secondes);

        if (time <= 0)
        {
            stopTimer = true;
            _couleur.color = Color.red;
        }

        if (time <= 6)
        {
            _couleur.color = Color.red;
        }

        if (stopTimer == false)
        {
            timerText.text = textTime;
            timeSlider.value = time;
        }
    }
}
