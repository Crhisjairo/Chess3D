using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script qui s'occupe de l'animation de l'horloge que les joueur ont dans leurs UI
public class ClockAnimation : MonoBehaviour
{
    //Initialisation de tous les variables que le script aura besoin.
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
//Enumerator qui fait commencer l'animation de l'horloge
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
//La méthode OnDestroy arrete tous les coroutines qui ont été faite dans le script
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
//Cette méthode arrete l'animation de l'horloge
    public void StopClockAnimation()
    {
        if (_animationCoroutine is null)
        {
            return;
        }
        
        StopCoroutine(_animationCoroutine);
    }
//Cette méthode recommence la coroutine pour l'animation de l'horloge
    public void ResumeClockAnimation()
    {
        _animationCoroutine = StartCoroutine(StartClockAnimation());
    }
}
