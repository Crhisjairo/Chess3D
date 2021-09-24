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
    /// Returne si la pièce est déjà seléctionnée.
    /// </summary>
    /// <returns>Si la pièce est seléctionnée</returns>
    public bool EstSelectionne { protected set; get; }
    
    
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
}
