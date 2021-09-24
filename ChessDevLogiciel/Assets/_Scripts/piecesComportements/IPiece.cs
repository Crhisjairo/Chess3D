using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
    
    /// <summary>
    /// Déplace la pièce.
    /// Selon le type de la pièce, celle-ci va se déplacer d'un façon ou une autre.
    /// </summary>
    /// <param name="caseDestination">Case de destination de la pièce</param>
    public void DeplacerPiece(Case caseDestination);

    /// <summary>
    /// Returne si la pièce peut être seléctionnée.
    /// </summary>
    /// <returns></returns>
    public bool PeutEtreSelectionne();
    
    /// <summary>
    /// Returne si la pièce est seléctionnée.
    /// </summary>
    /// <returns>Si la pièce est seléctionnée</returns>
    public bool EstSelectionne();

    /// <summary>
    /// Seléctionne la pièce et affiche les possibilités du mouvement.
    /// Chaque pièce va afficher des possibilités de mouvement différentes.
    /// </summary>
    public void SelectionnerPiece();
    
    /// <summary>
    /// Seléctionne la pièce et affiche les possibilités du mouvement.
    /// Chaque pièce va afficher des possibilités de mouvement différentes.
    /// </summary>
    public void DeselectionnerPiece();
}
