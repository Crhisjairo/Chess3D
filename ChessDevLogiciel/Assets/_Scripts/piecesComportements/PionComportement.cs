using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PionComportement : Piece
{
   private Rigidbody _rb;

   [SerializeField] private Vector2Int[] _moveSet = new Vector2Int[]
   {
      new Vector2Int(0, 2),
   };

   private void Start()
   {
      _rb = GetComponent<Rigidbody>();
      
      //On définit l'ensemble de mouvement de la pièce
      moveSet = _moveSet;
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
   }

   public override void SelectionnerPiece()
   {
      //On peut changer la couleur de la pièce si l'on veut
      EstSelectionne = true;
      //Il faut permettre seulement les déplacements possibles ici selon le type de pièce.
      //C'est le BoardManager qui activera les cases pour se déplacer selon le moveSet envoyé.
      
      //On affiche les cases possibles du pion pour se déplacer
      BoardManager.Instance.ActiverCasesRelativeTo(this);
      
   }

   public override void DeselectionnerPiece()
   {
      EstSelectionne = false;
      
      //On cache les cases disponibles
      //Il faudra se communiquer avec les cases du tableau.
      BoardManager.Instance.DesactiverCases(this); //le this est temporel
   }

}
