using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string PieceTag = "Piece";
    private const string CaseDuTableauTag = "CaseDuTableau";
    
    private IPiece pieceSelectionne;
    
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
            Debug.Log("Touché " + hit.collider.name);
            
            //Ici, il faut déplacer la pièce au centre de la case désiré.
            //On attents que les cases soient programées
            if (hit.collider.CompareTag(PieceTag))
            {
                SelectionnerNouvellePiece(hit);
            }

            if (hit.collider.CompareTag(CaseDuTableauTag))
            {
                if (pieceSelectionne is null)
                {
                    Debug.Log("Pas de pièce seléctionné");
                    return;
                }
                
                //On déplace la pièce et on la déseléctionne
                //On envoie la position du centre du collider de la case
                pieceSelectionne.DeplacerPiece(hit.collider.gameObject.GetComponent<Case>());
                pieceSelectionne.DeselectionnerPiece();

                pieceSelectionne = null; //On efface la reférence à la pièce selecctionné
                
            }
            
        }
    }

    private void SelectionnerNouvellePiece(RaycastHit hit)
    {
        IPiece nouvellePiece = hit.collider.GetComponent<IPiece>();

        //Si on n'a pas de pièce seléctionnée, on seléctionne la première pièce touchée.
        if (pieceSelectionne is null)
        {
            pieceSelectionne = nouvellePiece;
            pieceSelectionne.SelectionnerPiece();
            Debug.Log("toute nouvelle pièce");
            return;
        }
        
        //Si la pièce n'est pas la même pièce
        //On seléctionne la nouvelle pièce
        if (!pieceSelectionne.Equals(nouvellePiece))
        {
            Debug.Log("NOUVELLE pièce!");
         
            pieceSelectionne.DeselectionnerPiece();
            //On prends la nouvelle pièce
            pieceSelectionne = nouvellePiece;
            
            pieceSelectionne.SelectionnerPiece();
        }
        else
        {
            Debug.Log("Meme pièce");
        }
    }
}
