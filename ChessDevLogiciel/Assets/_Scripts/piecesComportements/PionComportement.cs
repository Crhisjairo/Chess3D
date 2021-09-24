using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PionComportement : Piece
{
   private Rigidbody _rb;

   private void Start()
   {
      _rb = GetComponent<Rigidbody>();
   }

   public override void DeplacerPiece(Case caseDestination)
   {
      //Il faut permettre seulement les déplacements possibles ici selon le type de pièce.
      //Dans ce cas, c'est un Pion
      
      
      //Dans le cas qu'il ait une pièce dans la case qu'on veut se déplacer,
      //on check si l'on peut la manger ou si c'est une de nos piece
      //pour ensuite se déplacer 
      
      
      //On déplace
      //On remplace la coordonnée  y  pour qu'elle reste intacte
      Vector3 destination = caseDestination.transform.position;
      
      destination.y = transform.position.y;
      _rb.MovePosition(destination);
      
      //Finalemment, on ajoute la reférence de cette pièce à la case où l'on se déplace
      caseDestination.SetPieceDansLaCase(this.gameObject);
   }

   public override void SelectionnerPiece()
   {
      EstSelectionne = true;
      
      //On affiche les cases possibles du pion pour se déplacer
      //Il faudra se communiquer avec les cases du tableau.
   }

   public override void DeselectionnerPiece()
   {
      EstSelectionne = false;
      
      //On cache les cases disponibles
      //Il faudra se communiquer avec les cases du tableau.
   }

}
