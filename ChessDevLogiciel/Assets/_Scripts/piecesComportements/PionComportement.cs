using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
<<<<<<< Updated upstream


public class PionComportement : Piece
{
=======
/**
 * Tous le code qui est présent dans la classe PionComportement sert à faire bouger le pion et comment le bouger
 */
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PionComportement : Piece
{
   /**
  * Dans cette partie du code, on initialise les variables qui vont être utilisés dans le script du comportement
  * du pion
  */
   private Rigidbody _rb;
>>>>>>> Stashed changes

   private Vector2Int[] _moveSet = new Vector2Int[]
   {
      new Vector2Int(0, 1), //Move normal du pion
      new Vector2Int(0, 2), //Move au départ
      new Vector2Int(1, 1),  //move pour manger en diagonal droit
      new Vector2Int(-1, 1)  //move pour manger en diagonal gauche
      

   };

   private bool isFirstMove;

   /**
     * Dans la méthode Start(), on set up tous les variables pour qu'elles prennent les composants dont elles vont avoir
     * comme le rigidbody.
     */
   
   private void Start()
   {
      _rb = GetComponent<Rigidbody>();
      _meshRenderer = GetComponent<MeshRenderer>();
      _boxCollider = GetComponent<BoxCollider>();
      _outline = GetComponent<Outline>();
      _outline.enabled = false; //On cache le outline au début.

      //On définit l'ensemble de mouvement de la pièce
      moveSet = _moveSet;
      isFirstMove = true;
   }
   /**
     * Dans la méthode SelectionnerPiece(), c'est ou tous les comportements se passe. Dans cette méthode nous faisons
     * que la reine puisse bouger selon les mouvements permis dans un jeu d'échecs réel. PLusieurs variables qui sont
     * initialiser dans d'autres scripts sont utilisés ici, la pluspart viennent du script BoardManager.
     */
   public override void SelectionnerPiece()
   {
      //On peut changer la couleur de la pièce icitte si l'on veut
      EstSelectionne = true; //On marque la pièce comme séléctionnée
<<<<<<< Updated upstream
      caseActuelle.SetEstActive(true); //On active la case où se trouve cette pièce pour l'allumer

      /*
       * Il faut permettre seulement les déplacements possibles ici selon le type de pièce.
       * C'est le BoardManager qui activera les cases (pour que la pièce puisse se déplacer) selon le moveSet envoyé.
       *
       * Le moveSet pourra être modifié avant d'activer les cases. Par exemple, le pion change son moveSet (ensemble de mouvements)
       * si une pièce se trouve dans un coin (car le pion mange en diagonale).
       * 
       * Dans ce cas, c'est un Pion, alors la logique du pion sera unique. Pourtant, vous pouvez vous baser sur cette
       * logique pour l'implémenter dans les autres pièces.
       */ 
      
=======
      
     
      //C'est le BoardManager qui activera les cases pour se déplacer selon le moveSet envoyé.
>>>>>>> Stashed changes
      Vector2Int coordonneesDeCetteCase = new Vector2Int();
      Joueur.NumeroJoueur joueurActuel = PlayersController.Instance._joueurActive.numeroJoueur;
      //On va utiliser les coodonnées d'une case relative au joueur. Si on ajoute plus de joueurs, le code reste flexible
      if (joueurActuel is Joueur.NumeroJoueur.Joueur1)
      {
         coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
      }
      else if (joueurActuel is Joueur.NumeroJoueur.Joueur2)
      {
         coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
      }
      
     
      
      if (isFirstMove)
      {
         Vector2Int firstMove = moveSet[1]; //On utilise le move pour le premier départ
         firstMove += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case

         for (int y = coordonneesDeCetteCase.y; y <= firstMove.y; y++)
         {
            BoardManager.Instance.ActiverCaseByCoord(firstMove.x, y, true, joueurActuel);
         }
         
      }
      
      //On check si on peut manger
      Vector2Int nextDiagonalMoveRight = moveSet[2]; //On utilise le move pour manger
      Vector2Int nextDiagonalMoveLeft = moveSet[3]; //On utilise le move pour manger
      nextDiagonalMoveRight += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case
      nextDiagonalMoveLeft += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case

      Piece pieceInNextCase; //Pièce qui va être retrouvé si jamais HasPieceOnCoord retourn vrai.

      if (BoardManager.Instance.HasPieceOnCoord(nextDiagonalMoveRight.x, nextDiagonalMoveRight.y, out pieceInNextCase))
      {
         //On check que la pièce dans la case qu'on va activer n'est pas une pièce qui appartient au joueur actuel.
         if (pieceInNextCase.JoueurProprietaire != joueurActuel)
         { 
            BoardManager.Instance.ActiverCaseByCoord(nextDiagonalMoveRight.x, nextDiagonalMoveRight.y, true, joueurActuel); 
         }
      }
      
      if (BoardManager.Instance.HasPieceOnCoord(nextDiagonalMoveLeft.x, nextDiagonalMoveLeft.y, out pieceInNextCase))
      {
         //On check que la pièce dans la case qu'on va activer n'est pas une pièce qui appartient au joueur actuel.
         if (pieceInNextCase.JoueurProprietaire != joueurActuel)
         { 
            BoardManager.Instance.ActiverCaseByCoord(nextDiagonalMoveLeft.x, nextDiagonalMoveLeft.y, true, joueurActuel);
         }
      }
      //On active les cases d'un déplacement normal
      Vector2Int nextMove = moveSet[0] + coordonneesDeCetteCase;
<<<<<<< Updated upstream

      //On vérifie qu'il n'ait aucune pièce en avant
      if (!BoardManager.Instance.HasPieceOnCoord(nextMove.x, nextMove.y, out pieceInNextCase))
      {
         BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, joueurActuel);
      }
      
      
=======
      BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, numeroJoueur);
      
      
>>>>>>> Stashed changes
   }

   
   
   /**
     * Dans la méthode DeplacerPiece(), on fait le deplacer de la piece selon la case qui a été choisi et on bouge la
     * piece avec la fonctionnalité de MovePosition qui vient avec le rigidbody
     */
   public override void DeplacerPiece(Case caseDestination)
   {
      //Dans le cas qu'il ait une pièce dans la case qu'on veut se déplacer,
      //on check si l'on peut la manger ou si c'est une de nos piece
      //pour ensuite se déplacer 
      
      //On déplace
      //On remplace la coordonnée  y  pour qu'elle reste intacte
      Vector3 destination = caseDestination.transform.position;
      
      destination.y = transform.position.y;
      _rb.MovePosition(destination);
      
      //Finalemment,
      //On efface la réference de la pièce dans la case
      //et on ajoute la reférence de cette pièce à la case où l'on se déplace
      //caseDestination.SetPieceDansLaCase(this);

      if (isFirstMove)
      {
         isFirstMove = false;
      }
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
