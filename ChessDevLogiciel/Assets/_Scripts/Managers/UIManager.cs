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
        //On �vite avoir deux instance de cette m�me classe lors du Awake
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
        //On d�bute avec le premier joueur
        int numeroJoueurQuiCommence = (int)Joueur.NumeroJoueur.Joueur1 - 1; //Enum qui se trouve dans Joueur
        _joueurActive = _joueurs[numeroJoueurQuiCommence];
        
        //On deshabilite toute les images au d�but
        DisablePiecesSlots(0, _slotPiecesPlayer1);
        DisablePiecesSlots(0, _slotPiecesPlayer2);
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
        //    Debug.Log("Le temps s'est termin�");
        //}

    }

    /// <summary>
    /// Met � jour les pi�ces mang�es du joueur pass� en param�tres.
    /// </summary>
    /// <param name="numeroJoueur">Num�ro du joueur � actualiser les pi�ces</param>
    /// <param name="piecesMangees">Liste des pi�ces mang�es</param>
    public void UpdatePlayerPieces(Joueur.NumeroJoueur numeroJoueur, List<Piece> piecesMangees)
    {
        if (numeroJoueur == Joueur.NumeroJoueur.Joueur1)
        {
            //On habilite les slots et on donne le sprite de la pi�ce
            for (int i = 0; i < piecesMangees.Count; i++)
            {
                _slotPiecesPlayer1[i].enabled = true;
                _slotPiecesPlayer1[i].sprite = piecesMangees[i].GetPieceSprite();
            }
            
            //On d�shabilite les slots qui n'ont pas de pi�ces
            DisablePiecesSlots(piecesMangees.Count, _slotPiecesPlayer1);
        }
        else if (numeroJoueur == Joueur.NumeroJoueur.Joueur2)
        {
            //On habilite les slots et on donne le sprite de la pi�ce
            for (int i = 0; i < piecesMangees.Count; i++)
            {
                _slotPiecesPlayer2[i].enabled = true;
                _slotPiecesPlayer2[i].sprite = piecesMangees[i].GetPieceSprite();
            }
            
            //On d�shabilite les slots qui n'ont pas de pi�ces
            DisablePiecesSlots(piecesMangees.Count, _slotPiecesPlayer2);
        }
        
    }

    private void DisablePiecesSlots(int startIndex, Image[] playerSlots)
    {
        //On d�shabilite les slots qui n'ont pas de pi�ces
        for (int i = startIndex; i < playerSlots.Length; i++)
        {
            playerSlots[i].enabled = false;
        }
    }

}
