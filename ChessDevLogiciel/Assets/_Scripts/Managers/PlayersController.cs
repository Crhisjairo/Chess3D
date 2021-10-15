using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    private const string PieceTag = "Piece";
    private const string CaseDuTableauTag = "CaseDuTableau";
    
    public static PlayersController Instance { private set; get;}
    
    private Piece _pieceSelectionne;

    public Joueur _joueurActive;
    [SerializeField] private Joueur[] _joueurs;

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
        //On débute avec le premier joueur
        int numeroJoueurQuiCommence = (int) Joueur.NumeroJoueur.Joueur1 - 1; //Enum qui se trouve dans Joueur
        _joueurActive = _joueurs[numeroJoueurQuiCommence]; 
        _joueurActive.SetPiecesActives(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Vérification du click de la souris
        if (Input.GetMouseButtonDown(0))
        {
            DeplacerPionAuClickPosition();
        }
    }
    private void DeplacerPionAuClickPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Touché " + hit.collider.name);
            
            //On vérifie s'il s'agit d'une pièce ou une case
            if (hit.collider.CompareTag(PieceTag))
            {
                SelectionnerNouvellePiece(hit);
                //Une fois que la pièce est séléctionnée, des cases sont activées
            }

            if (hit.collider.CompareTag(CaseDuTableauTag))
            {
                //On check s'il y a une pièce selectionnée
                if (_pieceSelectionne is null)
                {
                    Debug.Log("Pas de pièce seléctionné");
                    return;
                }

                //Ici, il faut déplacer la pièce au centre de la case désiré.
                //On déplace la pièce, on la déseléctionne et on change de tour
                //On envoie la position du centre du collider de la case
                Case caseDestination = hit.collider.gameObject.GetComponent<Case>();
                
                //On check que la pièce soit active
                if (!caseDestination.EstActive())
                {
                    Debug.Log("Case pas active ");
                    return;
                }

                //Checker si on peut manger la pièce
                //POUR DEBUG, EFFACER APRÈS
                
                /*
                if (caseDestination.HasPiece())
                {
                    return;
                }

                */
                
                
                _pieceSelectionne.DeplacerPiece(caseDestination);
                _pieceSelectionne.DeselectionnerPiece();

                _pieceSelectionne = null; //On efface la reférence à la pièce selecctionné
                
                ChangerTour(); //Finalement, on change de tour.
            }
            
        }
    }

    private void SelectionnerNouvellePiece(RaycastHit hit)
    {
        Piece nouvellePiece = hit.collider.GetComponent<Piece>();

        //Si on n'a pas de pièce seléctionnée, on seléctionne la première pièce touchée.
        if (_pieceSelectionne is null)
        {
            _pieceSelectionne = nouvellePiece;
            
            //Si la pièce n'est pas active, on fait rien
            if (!_pieceSelectionne.EstActive)
            {
                Debug.Log("Pièce pas active");
                _pieceSelectionne = null;
                return;
            }
            
            _pieceSelectionne.SelectionnerPiece();
            Debug.Log("toute nouvelle pièce");
            return;
        }
        
        //Si la pièce n'est pas la même pièce
        //On seléctionne la nouvelle pièce
        if (!_pieceSelectionne.Equals(nouvellePiece) && nouvellePiece.EstActive)
        {
            
            Debug.Log("NOUVELLE pièce!");
         
            _pieceSelectionne.DeselectionnerPiece();
            //On prends la nouvelle pièce
            _pieceSelectionne = nouvellePiece;
            
            _pieceSelectionne.SelectionnerPiece();
        }
        else
        {
            Debug.Log("Meme pièce ou pièce pas active");
        }
    }

    /// <summary>
    /// Chaque fois qu'on change de joueur.
    /// </summary>
    /// <param name="numeroJoueur">Numero de joueur</param>
    private void ChangerTour()
    {
        //On désactive les pièces du joueur active.
        _joueurActive.SetPiecesActives(false);
        
        //On change le joueur active.
        int nouveauNumeroJoueur = ((int) _joueurActive.numeroJoueur) + 1;
        //On vérifie qu'on dépasse pas les nombre max des joueurs
        if (nouveauNumeroJoueur > _joueurs.Length )
            nouveauNumeroJoueur = (int) Joueur.NumeroJoueur.Joueur1;
        
        _joueurActive = _joueurs[nouveauNumeroJoueur - 1];
        Debug.Log("Le tour à joueur " + nouveauNumeroJoueur);

        //On active les pièce du nouveau joueur qui es maintenant joueurActive.
        _joueurActive.SetPiecesActives(true);
        
        GameManager.Instance.ChangerCameraTo(_joueurActive.numeroJoueur);
    }
}