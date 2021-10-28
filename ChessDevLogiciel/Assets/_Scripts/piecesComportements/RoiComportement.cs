using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream

public class RoiComportement : Piece
{

    private Vector2Int[] _moveSet = new Vector2Int[]
    {
      new Vector2Int(0, 1), //Move vers le haut
      new Vector2Int(1, 1),  //move vers diagonal droit haut
      new Vector2Int(-1, -1),  //move vers diagonal gauche bas
      new Vector2Int(-1, 1),  //move vers diagonal gauche haut
      new Vector2Int(1, -1),//move vers diagonal droit bas
      new Vector2Int(0, -1), //move vers bas
      new Vector2Int(1, 0), //move vers la gauche
      new Vector2Int(-1, 0) //move vers la droite
    };
    
=======
/**
 * Tous le code dans la classe RoiComportement sert pour faire bouger le Roi et lui dire comment bouger aussi.
 */
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class RoiComportement : Piece
{
    /**
   * Dans cette partie du code, on initialise les variables qui vont être utilisés dans le script du comportement
   * du Roi
   */
    private Rigidbody _rb;

    private Vector2Int[] _moveSet = new Vector2Int[]
    {
      new Vector2Int(0, 1), //Move au départ
      new Vector2Int(1, 1),  //move pour manger en diagonal droit
      new Vector2Int(-1, -1),  //move pour manger en diagonal en bas droite
      new Vector2Int(-1, 1),  //move pour manger en diagonal gauche
      new Vector2Int(1, -1),//move pour manger en diagonal en bas gauche
      new Vector2Int(0, -1) //move pour diagonale derriere
                            
    };

    /**
     * Dans la méthode Start(), on set up tous les variables pour qu'elles prennent les composants dont elles vont avoir
     * comme le rigidbody.
     */

>>>>>>> Stashed changes
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false; //On cache le outline au début.

        //On définit l'ensemble de mouvement de la pièce
        moveSet = _moveSet;
    }
    /**
     * Dans la méthode SelectionnerPiece(), c'est ou tous les comportements se passe. Dans cette méthode nous faisons
     * que le roi puisse bouger selon les mouvements permis dans un jeu d'échecs réel. PLusieurs variables qui sont
     * initialiser dans d'autres scripts sont utilisés ici, la pluspart viennent du script BoardManager.
     */
    public override void SelectionnerPiece()
    {
        //On peut changer la couleur de la pièce icitte si l'on veut
        EstSelectionne = true; //On marque la pièce comme séléctionnée
        caseActuelle.SetEstActive(true); //On active la case où se trouve cette pièce pour l'allumer

      
        //C'est le BoardManager qui activera les cases pour se déplacer selon le moveSet envoyé.
        Vector2Int coordonneesDeCetteCase = new Vector2Int();
        Joueur.NumeroJoueur numeroJoueur = PlayersController.Instance._joueurActive.numeroJoueur;
        //On va utiliser les coodonnées d'une case relative au joueur. Si on ajoute plus de joueurs, le code reste flexible
        //Ce code détermine quel joueur est en train de bouger sa reine,
        //C'est avec la variable Joueur, qui vient du script PlayersController
        if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
        }
        else if (numeroJoueur is Joueur.NumeroJoueur.Joueur2)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
        }

<<<<<<< Updated upstream
        //C'EST TOUTE CETTE PARTIE qui change SELON la pièce
        Vector2Int nextMove;
        Piece pieceInNextCase; //Pièce qui va être retrouvé si jamais HasPieceOnCoord retourn vrai.
        Joueur.NumeroJoueur joueurActuel = PlayersController.Instance._joueurActive.numeroJoueur;
=======
        
>>>>>>> Stashed changes
        
        for (int moveSetIndex = 0; moveSetIndex < moveSet.Length; moveSetIndex++)
        {
            nextMove = moveSet[moveSetIndex] + coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case

            if (BoardManager.Instance.HasPieceOnCoord(nextMove.x, nextMove.y, out pieceInNextCase))
            {
                //S'il s'agit d'un ennemie
                if (pieceInNextCase.JoueurProprietaire != joueurActuel)
                {
                    //On active les cases par coordonn�es dans le board
                    BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, joueurActuel);
                }
            }
            else
            {
                //On active les cases par coordonn�es dans le board
                BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, joueurActuel);
            }
            
        }
<<<<<<< Updated upstream
        
=======
       
>>>>>>> Stashed changes
    }
    /**
     * Dans la méthode DeplacerPiece(), on fait le deplacer de la piece selon la case qui a été choisi et on bouge la
     * piece avec la fonctionnalité de MovePosition qui vient avec le rigidbody
     */
    public override void DeplacerPiece(Case caseDestination)
    {
        //On déplace
        //On remplace la coordonnée  y  pour qu'elle reste intacte
        Vector3 destination = caseDestination.transform.position;

        destination.y = transform.position.y;
        _rb.MovePosition(destination);

        //Finalemment,
        //On efface la réference de la pièce dans la case
        //et on ajoute la reférence de cette pièce à la case où l'on se déplace
        //caseDestination.SetPieceDansLaCase(this);
    }
    /**
     * La méthode DeselectionnerPiece() sert à déselectionner un pièce lorsqu'on a plus besoin.
     * Les cases vont se desactiver donc elles ne seront plus rouge.
     */
    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;

        //On dit au board de désactiver les cases actives
        BoardManager.Instance.DesactiverCases(); //le this est temporel
    }

}