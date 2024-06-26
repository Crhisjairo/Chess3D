using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersController : MonoBehaviour
{
    //Initialisation des variables utiliser dans le script
    private const string PieceTag = "Piece";
    private const string CaseDuTableauTag = "CaseDuTableau";

    [SerializeField] private Animator _drivenCamAnimator;

    public static PlayersController Instance { private set; get; }

    public Piece _pieceSelectionne;

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

    // Update is called once per frame
    void Update()
    {
        //Vérification du click de la souris
        if (Input.GetMouseButtonDown(0))
        {
            DeplacerPionAuClickPosition();
        }
    }

    private void LoadAndSetPlayerData()
    {
        //TODO modifier ça pour le faire en fonction des données de la BD à distance
    }

    public void SetPlayersSettingsAndStartGame(PlayerData playerData, float secondesPourJoueur)
    {
        //On débute avec le premier joueur, joueur local
        int numeroJoueurQuiCommence = (int)Joueur.NumeroJoueur.Joueur1 - 1; //Enum qui se trouve dans Joueur
        _joueurActive = _joueurs[numeroJoueurQuiCommence];
        _joueurActive.SetPiecesActives(true);

        UIManager.Instance.UpdatePlayersTurn(_joueurActive);


        _joueurs[0].SetProperties(playerData, secondesPourJoueur);
        _joueurs[1].SetProperties(null, secondesPourJoueur);
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

                //Checker s'il y a une piece dans la case qu'on veut se déplacer pour la manger.
                if (caseDestination.HasPiece())
                {
                    MangerPieceAtCase(caseDestination);
                }

                //Finalement, on se déplace.
                DeplacerPieceToCase(caseDestination);
                ChangerTour(); //on change de tour.

            }

        }
    }

    /// <summary>
    /// On prends la pièce qui se trouve dans la case où l'on veut se déplacer et on l'ajoute à la liste de
    /// piècesMangees du joueur contraire.
    /// </summary>
    /// <param name="caseDestination"></param>
    private void MangerPieceAtCase(Case caseDestination)
    {
        Piece pieceMangee = caseDestination.GetPieceDansLaCase();
        caseDestination.SetPieceDansLaCase(null);

        _joueurActive.AjouterPieceMangee(pieceMangee);

        Debug.Log("La pièce mangée est " + pieceMangee.name);

        //JUSTE POUR DEBUG
        Debug.Log("Pièce mangées du " + _joueurActive.numeroJoueur + " sont:");
        foreach (var piece in _joueurActive.GetPiecesMangees())
        {
            Debug.Log("Nom: " + piece.name + " estActive: " + piece.GetEstActive());
        }

        pieceMangee.CacherPiece();
     
        if ((pieceMangee.name.Equals("Chess King White") || pieceMangee.name.Equals("Chess King Black"))) 
        {
            SceneManager.LoadScene("Victoire");
        }

        UIManager.Instance.UpdatePlayerPieces(_joueurActive.numeroJoueur, _joueurActive.GetPiecesMangees());
    }

    private void DeplacerPieceToCase(Case caseDestination)
    {
        _pieceSelectionne.DeplacerPiece(caseDestination);
        _pieceSelectionne.DeselectionnerPiece();

        _pieceSelectionne = null; //On efface la reférence à la pièce selecctionné
    }

    private void SelectionnerNouvellePiece(RaycastHit hit)
    {
        Piece nouvellePiece = hit.collider.GetComponent<Piece>();

        //Si on n'a pas de pièce seléctionnée, on seléctionne la première pièce touchée.
        if (_pieceSelectionne is null)
        {
            _pieceSelectionne = nouvellePiece;
            
            //Si la pièce n'est pas active, on fait rien
            if (!_pieceSelectionne.GetEstActive())
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
        if (!_pieceSelectionne.Equals(nouvellePiece) && nouvellePiece.GetEstActive())
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

        //On active les pièce du nouveau joueur qui est maintenant joueurActive.
        _joueurActive.SetPiecesActives(true);
        
        ChangerCameraTo(_joueurActive.numeroJoueur);
        //On modifie le UI
        UIManager.Instance.UpdatePlayersTurn(_joueurActive);
    }
    
    public void ChangerCameraTo(Joueur.NumeroJoueur numeroJoueur)
    {
        Debug.Log("Changement de cam à " + numeroJoueur);
        
        //Faut changer la camera par Cinemachine Virtual Camera
        if (numeroJoueur == Joueur.NumeroJoueur.Joueur1)
        {
            _drivenCamAnimator.Play("vcamPlayer1");
        } 
        else if (numeroJoueur == Joueur.NumeroJoueur.Joueur2)
        {
            _drivenCamAnimator.Play("vcamPlayer2");
        } //On peut ajouter d'autres caméras pour d'autres joueurs. Il faudra adapter l'animator et le stateDrivenCam
    }

    public void SetJoueurs(Joueur[] joueurs)
    {
        _joueurs = joueurs;
    }
}
