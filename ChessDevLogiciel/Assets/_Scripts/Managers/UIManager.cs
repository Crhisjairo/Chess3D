using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { private set; get; }

    //[SerializeField] private Text _tempsRestantText;
    //private float _tempsJeu = 300f;
    //public float _tempsRestant;
    //public bool _estArrete;
    //private string _tempsText;

    public Joueur _joueurActive;
    [SerializeField] private Joueur[] _joueurs;
    
    [SerializeField] private Image[] _slotPiecesPlayer1;
    [SerializeField] private Image[] _slotPiecesPlayer2;
    
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
        //On débute avec le premier joueur
        int numeroJoueurQuiCommence = (int)Joueur.NumeroJoueur.Joueur1 - 1; //Enum qui se trouve dans Joueur
        _joueurActive = _joueurs[numeroJoueurQuiCommence];
        
    }

    // Update is called once per frame
    void Update()
    {
        //_tempsRestant = _tempsJeu - Time.time;
        //int minutes = Mathf.FloorToInt(_tempsRestant / 60);
        //int secondes = Mathf.FloorToInt(_tempsRestant - minutes * 60f);
        //_tempsText = string.Format("{0:00}:{1:00}", minutes, secondes);

        //if (_estArrete == false)
        //{
        //    _tempsRestantText.text = _tempsText;
        //}

        //if (_tempsRestant <= 0)
        //{
        //    _estArrete = true;
        //    Debug.Log("Le temps s'est terminé");
        //}

    }

    public void UpdatePlayerPieces(Joueur.NumeroJoueur numeroJoueur, List<Piece> piecesMangees)
    {
        if (numeroJoueur == Joueur.NumeroJoueur.Joueur1)
        {
            //On habilite les slots et on donne le sprite de la pièce
            
            
            //On déshabilite les slots qui n'ont pas de pièces
            for (int i = piecesMangees.Count; i < _slotPiecesPlayer1.Length; i++)
            {
                _slotPiecesPlayer1[i].enabled = false;
            }
        }
        else if (numeroJoueur == Joueur.NumeroJoueur.Joueur2)
        {
            
        }
        
    }

}
