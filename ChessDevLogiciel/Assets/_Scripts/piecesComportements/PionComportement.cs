using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PionComportement : MonoBehaviour, IPiece
{
   /*
    * Dans cette section du code, c'est ou nous allons initialiser tous les variables dont nous allons avoir besoin dans notre script.
    */
   private Rigidbody _rb;

   private bool _isSelected;
   
   private void Start()
   {
      _rb = GetComponent<Rigidbody>();
   }

   public void DeplacerPiece(Vector3 destination)
   {
      //Il faut calculer les déplacement possibles ici.
      //Dans ce cas, c'est un Pion
      
      _rb.MovePosition(destination);
   }
   
   public void SelectionnerPiece()
   {
      _isSelected = true;
      
      //On affiche les cases possibles du pion pour se déplacer
   }

   public void DeselectionnerPiece()
   {
      _isSelected = false;
      
      //On cache les cases disponibles
   }

   public bool IsSelected()
   {
      return _isSelected;
   }

   
}
