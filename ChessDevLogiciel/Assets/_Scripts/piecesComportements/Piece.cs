using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Piece : MonoBehaviour
{
    /// <summary>
    /// Returne si la pièce est activée et peut être seléctionnée.
    /// </summary>
    /// <returns></returns>
    public bool EstActive { set; get; }
    
    /// <summary>
    /// Ensemble de mouvement rélatives à la pièce.
    /// </summary>
    [HideInInspector] public Vector2Int[] moveSet;
    
    /// <summary>
    /// Returne si la pièce est déjà seléctionnée.
    /// </summary>
    /// <returns>Si la pièce est seléctionnée</returns>
    public bool EstSelectionne { protected set; get; }
    
    public Case caseActuelle;
    
    //Méthodes qui doivent être redéfiniées, car c'est un comportement propre à la pièce.
    
    /// <summary>
    /// Déplace la pièce.
    /// Selon le type de la pièce, celle-ci va se déplacer d'un façon ou une autre.
    /// </summary>
    /// <param name="caseDestination">Case de destination de la pièce</param>
    public abstract void DeplacerPiece(Case caseDestination);
    
    /// <summary>
    /// Seléctionne la pièce et affiche les possibilités du mouvement.
    /// Chaque pièce va afficher des possibilités de mouvement différentes.
    /// </summary>
    public abstract void SelectionnerPiece();
    
    /// <summary>
    /// Seléctionne la pièce et affiche les possibilités du mouvement.
    /// Chaque pièce va afficher des possibilités de mouvement différentes.
    /// </summary>
    public abstract void DeselectionnerPiece();

    private void Update()
    {
        //Pour voir le rayon utilisé pour trouver la case en dessous
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 2, Color.yellow);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Chaque fois qu'on entre dans une case, on sauvegarde la référence.
        if (other.CompareTag("CaseDuTableau"))
        {
            if (caseActuelle != null)
            {
               caseActuelle.SetPieceDansLaCase(null); //On efface la référence de la case 
            }

            caseActuelle = other.GetComponent<Case>(); //On s'ajoute au nouvelle case
            caseActuelle.SetPieceDansLaCase(this);
            
            //Debug.Log("Collision: " + caseActuelle.numeroDeCase);
        }
    }

}
