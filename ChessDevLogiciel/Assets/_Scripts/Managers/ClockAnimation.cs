using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockAnimation : MonoBehaviour
{
    private int _spriteIndex;

    private Coroutine _animationCoroutine;
    
    private Image _image;
    public Sprite[] Sprites;

    public float secondsToWait;
    // Start is called before the first frame update
    void Start()
    {
        _spriteIndex = 0;
        
        _image = GetComponent<Image>();
        
        //_animationCoroutine = StartCoroutine(StartClockAnimation());
    }

    private IEnumerator StartClockAnimation()
    {
        while (true)
        {
            for (int i = _spriteIndex; i < Sprites.Length; i++)
            {
                _image.sprite = Sprites[i];
                _spriteIndex = i;
                                
                yield return new WaitForSeconds(secondsToWait);
            }

            _spriteIndex = 0;
        }
       
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void StopClockAnimation()
    {
        if (_animationCoroutine is null)
        {
            return;
        }
        
        StopCoroutine(_animationCoroutine);
    }

    public void ResumeClockAnimation()
    {
        _animationCoroutine = StartCoroutine(StartClockAnimation());
    }
}
