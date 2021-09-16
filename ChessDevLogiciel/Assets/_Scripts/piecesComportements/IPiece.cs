using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
    
    /// <summary>
    /// Déplace la pièce.
    /// Selon le type de la pièce, celle-ci va se déplacer d'un façon ou une autre.
    /// </summary>
    /// <param name="destination">Position de destination de la pièce</param>
    public void DeplacerPiece(Vector3 destination);

    /// <summary>
    /// Returne si la pièce est seléctionne.
    /// </summary>
    /// <returns>Si la pièce est seléctionnée</returns>
    public bool IsSelected();

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
