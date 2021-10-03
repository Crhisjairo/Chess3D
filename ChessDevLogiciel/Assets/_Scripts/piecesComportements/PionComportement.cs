using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PionComportement : Piece
{
   private Rigidbody _rb;

   private Vector2Int[] _moveSet = new Vector2Int[]
   {
      new Vector2Int(0, 1), //Move normal du pion
      new Vector2Int(0, 2), //Move au départ
      new Vector2Int(1, 1),  //move pour manger en diagonal droit
      new Vector2Int(-1, 1)  //move pour manger en diagonal gauche
      //new Vector2Int(2, 1) //Exemple d'un mouvement en L (cheval)
      //new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0) //Exemple de mouvement vers toute la droite
      //new Vector2Int(-BoardManager.MAX_BOARD_SIZE, 0) //Exemple de mouvement vers toute la gauche
   };

   private bool isFirstMove;

   private void Start()
   {
      _rb = GetComponent<Rigidbody>();
      
      //On définit l'ensemble de mouvement de la pièce
      moveSet = _moveSet;
      isFirstMove = true;
   }
   
   public override void SelectionnerPiece()
   {
      //On peut changer la couleur de la pièce icitte si l'on veut
      caseActuelle.SetEstActive(true); //On active la case où se trouve cette pièce pour l'allumer
      EstSelectionne = true; //On marque la pièce comme séléctionnée
      
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
      
      Vector2Int coordonneesDeCetteCase = new Vector2Int();
      Joueur.NumeroJoueur numeroJoueur = PlayerController.Instance._joueurActive.numeroJoueur;
      //On va utiliser les coodonnées d'une case relative au joueur. Si on ajoute plus de joueurs, le code reste flexible
      if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
      {
         coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
      }
      else if (numeroJoueur is Joueur.NumeroJoueur.Joueur2)
      {
         coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
      }
      
      //C'EST TOUTE CETTE PARTIE qui change SELON la pièce
      
      if (isFirstMove)
      {
         Vector2Int firstMove = moveSet[1]; //On utilise le move pour le premier départ
         firstMove += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case

         for (int y = coordonneesDeCetteCase.y; y <= firstMove.y; y++)
         {
            BoardManager.Instance.ActiverCaseByCoord(firstMove.x, y, true, numeroJoueur);
         }
         
      }
      //On check si on peut manger
      Vector2Int nextDiagonalMoveRight = moveSet[2]; //On utilise le move pour manger
      Vector2Int nextDiagonalMoveLeft = moveSet[3]; //On utilise le move pour manger
      nextDiagonalMoveRight += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case
      nextDiagonalMoveLeft += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case

      if (BoardManager.Instance.HasPieceOnCoord(nextDiagonalMoveRight.x, nextDiagonalMoveRight.y))
      {
         BoardManager.Instance.ActiverCaseByCoord(nextDiagonalMoveRight.x, nextDiagonalMoveRight.y, true, numeroJoueur);
      }
      
      if (BoardManager.Instance.HasPieceOnCoord(nextDiagonalMoveLeft.x, nextDiagonalMoveLeft.y))
      {
         BoardManager.Instance.ActiverCaseByCoord(nextDiagonalMoveLeft.x, nextDiagonalMoveLeft.y, true, numeroJoueur);
      }
      //On active les cases d'un déplacement normal
      Vector2Int nextMove = moveSet[0] + coordonneesDeCetteCase;
      BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, numeroJoueur);
      
      /*
       //AUTRE EXEMPLE DE MOUVEMENT POUR UNE AUTRE PIÈCE//
       //C'EST CETTE PARTIE qui change SELON la pièce
      //On repète pour tous les mouvements possibles dans _actualMoveSet
      for (int i = 0; i < moveSet.Length; i++)
      {
         Vector2Int nextMove = moveSet[i];
         nextMove += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case
         
         
         //On active dans les coordonnées x positif
         for (int xPosi = coordonneesDeCetteCase.x; xPosi <= nextMove.x; xPosi++)
         {
            //On active les cases par coordonnées dans le board
            BoardManager.Instance.ActiverCaseByCoord(xPosi, coordonneesDeCetteCase.y, true, numeroJoueur);
         }
         //On active dans les coordonnées x negatif
         for (int xNega = coordonneesDeCetteCase.x; xNega >= nextMove.x; xNega--)
         {
            //On active les cases par coordonnées dans le board
            BoardManager.Instance.ActiverCaseByCoord(xNega, coordonneesDeCetteCase.y, true, numeroJoueur);
         }

         //On active dans les coordonnées y positif
         for (int yPosi = coordonneesDeCetteCase.y; yPosi <= nextMove.y; yPosi++)
         {
            //On active les cases par coordonnées dans le board
            BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, numeroJoueur);
            Debug.Log(nextMove.x + ":" + yPosi);
         }
         //On active dans les coordonnées y negatif
         for (int yPosi = coordonneesDeCetteCase.y; yPosi >= nextMove.y; yPosi--)
         {
            //On active les cases par coordonnées dans le board
            BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, numeroJoueur);
            Debug.Log(nextMove.x + ":" + yPosi);
         }
      }
      */
   }

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

   

   public override void DeselectionnerPiece()
   {
      EstSelectionne = false;
      
      //On dit au board de désactiver les cases actives
      BoardManager.Instance.DesactiverCases(); //le this est temporel
   }

}
