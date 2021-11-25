using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script qui s'occupe de tous le UI
public class UIManager : MonoBehaviour
{
    //Initialisation de tous les variables qui vont être utilisés dans le script
    public static UIManager Instance { private set; get; }

    //[SerializeField] private Text _tempsRestantText;
    //private float _tempsJeu = 300f;
    //public float _tempsRestant;
    //public bool _estArrete;
    //private string _tempsText;

    public Joueur _joueurActive;
    [SerializeField] private Joueur[] _joueurs;

    [SerializeField] private Canvas _canvasJoueur1, _canvasJoueur2;
    [SerializeField] private ClockAnimation _clockJoueur1, _clockJoueur2;
    
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
        
        //On deshabilite toute les images au début
        DisablePiecesSlots(0, _slotPiecesPlayer1);
        DisablePiecesSlots(0, _slotPiecesPlayer2);
        
        _canvasJoueur1.transform.localScale = new Vector3(_canvasJoueur1.transform.localScale.x - 0.3f, _canvasJoueur1.transform.localScale.y - 0.3f, _canvasJoueur1.transform.localScale.z);
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

    public void UpdatePlayersTurn(Joueur joueur)
    {
        _joueurActive = joueur;

        //ResizePlayerCanvas();
        StopAndStartPlayerClock(joueur);
        
    }

    private void StopAndStartPlayerClock(Joueur joueur)
    {
        //On arrête le clock contraire
        if (joueur.numeroJoueur == Joueur.NumeroJoueur.Joueur1)
        {
            _clockJoueur1.ResumeClockAnimation();
            _joueurs[0].SetTimerOn(true);
            
            _clockJoueur2.StopClockAnimation();
            _joueurs[1].SetTimerOn(false);

        } else if (joueur.numeroJoueur == Joueur.NumeroJoueur.Joueur2)
        {
            _clockJoueur2.ResumeClockAnimation();
            _joueurs[1].SetTimerOn(true);
            
            _clockJoueur1.StopClockAnimation();
            _joueurs[0].SetTimerOn(false);
        }
    }

    private void ResizePlayerCanvas()
    {
        Vector3 startCanvas1Pos = _canvasJoueur1.transform.localScale;
        Vector3 startCanvas2Pos = _canvasJoueur2.transform.localScale;
        
        //TODO Changer ça pour LeanTween.
        //On met en emphasis le joueur à qui est le tour
        float xScaleCurrentPlayer;
        float yScaleCurrentPlayer;
        float zScaleCurrentPlayer;
        
        float xScalePastPlayer = 0;
        float yScalePastPlayer = 0;
        float zScalePastPlayer = 0;

        Canvas canvasToMagnify = null;
        Canvas canvasToReduce = null;
        
        if (_joueurActive.numeroJoueur == Joueur.NumeroJoueur.Joueur1)
        {
            canvasToMagnify = _canvasJoueur1;
            canvasToReduce = _canvasJoueur2;
            
        } else if (_joueurActive.numeroJoueur == Joueur.NumeroJoueur.Joueur2)
        {
            canvasToMagnify = _canvasJoueur2;
            canvasToReduce = _canvasJoueur1;
        }

        xScaleCurrentPlayer = canvasToMagnify.transform.localScale.x;
        yScaleCurrentPlayer = canvasToMagnify.transform.localScale.y;
        zScaleCurrentPlayer = canvasToMagnify.transform.localScale.z;
        
        xScalePastPlayer = canvasToReduce.transform.localScale.x;
        yScalePastPlayer = canvasToReduce.transform.localScale.y;
        zScalePastPlayer = canvasToReduce.transform.localScale.z;


        canvasToMagnify.transform.localScale = new Vector3(xScaleCurrentPlayer + 0.3f, yScaleCurrentPlayer + 0.3f, zScaleCurrentPlayer);
        canvasToReduce.transform.localScale = new Vector3(xScalePastPlayer - 0.3f, yScalePastPlayer - 0.3f, zScalePastPlayer);
        
    }

    /// <summary>
    /// Met à jour les pièces mangées du joueur passé en paramètres.
    /// </summary>
    /// <param name="numeroJoueur">Numéro du joueur à actualiser les pièces</param>
    /// <param name="piecesMangees">Liste des pièces mangées</param>
    public void UpdatePlayerPieces(Joueur.NumeroJoueur numeroJoueur, List<Piece> piecesMangees)
    {
        if (numeroJoueur == Joueur.NumeroJoueur.Joueur1)
        {
            //On habilite les slots et on donne le sprite de la pièce
            for (int i = 0; i < piecesMangees.Count; i++)
            {
                _slotPiecesPlayer1[i].enabled = true;
                _slotPiecesPlayer1[i].sprite = piecesMangees[i].GetPieceSprite();
            }
            
            //On déshabilite les slots qui n'ont pas de pièces
            DisablePiecesSlots(piecesMangees.Count, _slotPiecesPlayer1);
        }
        else if (numeroJoueur == Joueur.NumeroJoueur.Joueur2)
        {
            //On habilite les slots et on donne le sprite de la pièce
            for (int i = 0; i < piecesMangees.Count; i++)
            {
                _slotPiecesPlayer2[i].enabled = true;
                _slotPiecesPlayer2[i].sprite = piecesMangees[i].GetPieceSprite();
            }
            
            //On déshabilite les slots qui n'ont pas de pièces
            DisablePiecesSlots(piecesMangees.Count, _slotPiecesPlayer2);
        }
        
    }

    private void DisablePiecesSlots(int startIndex, Image[] playerSlots)
    {
        //On déshabilite les slots qui n'ont pas de pièces
        for (int i = startIndex; i < playerSlots.Length; i++)
        {
            playerSlots[i].enabled = false;
        }
    }

}
