using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get;}
    
    public delegate void DeshabiliterPiecesJoueur1();
    public DeshabiliterPiecesJoueur1 deshabiliterPiecesJoueur1;
    
    private List<IPiece> _piecesJoueur1;
    private List<IPiece> _piecesJoueur2;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
