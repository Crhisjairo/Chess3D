using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockAnimation : MonoBehaviour
{
    private Image _image;
    public Sprite[] Sprites;

    public float secondsToWait;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        
        StartCoroutine(StartClockAnimation());
    }

    private IEnumerator StartClockAnimation()
    {
        while (true)
        {
            foreach (Sprite sprite in Sprites)
            {
                _image.sprite = sprite;
                yield return new WaitForSeconds(secondsToWait);
            } 
        }
       
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
