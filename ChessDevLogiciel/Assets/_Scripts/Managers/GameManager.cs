using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui va se charger du fonctionnement du jeu
public class GameManager : MonoBehaviour
{
    //Initialisation de tous les variables que nous aurons besoin dans ce script
    public static GameManager Instance { private set; get;}

    
    private void Awake()
    {
        //On évite avoir deux instance de cette même classe lors du Awake
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); //On destroy le gameObject qui contient ce script. Faire attention.
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
       
    }

  
    
   
}
