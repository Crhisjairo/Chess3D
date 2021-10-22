using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe qui exécute le comportment du cheval et ses déplacements dans le jeu
/// </summary>
public class ChevalierComportement : Piece
{
    // Liste de tous les déplacements possibles du chevalier
    private Vector2Int[] _moveSet = new Vector2Int[]
    {
        new Vector2Int(1, 2), // 1 pas vers la droite, 2 pas vers le haut
        new Vector2Int(-1, 2), // 1 pas vers la gauche, 2 pas vers le haut
        new Vector2Int(2, -1), // 2 pas vers la droite, 1 pas vers le bas
        new Vector2Int(-2, -1), // 2 pas vers la gauche, 1 pas vers le bas
        new Vector2Int(1, -2), // 1 pas vers la droite, 2 pas vers le bas
        new Vector2Int(-1, -2), // 1 pas vers la gauche, 2 pas vers le bas
        new Vector2Int(2, 1), // 2 pas vers la droite, 1 pas vers le haut
        new Vector2Int(-2, 1) // 2 pas vers la gauche, 1 pas vers le haut
    };

    private Vector2Int _nextMove; // Prochain déplacement du chevalier

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false; //On cache le outline au début.

        moveSet = _moveSet; //On définit l'ensemble de mouvement de la pièce
    }

    /// <summary>
    /// Méthode qui permet de seléctionner un cheval et active les cases où il
    /// peut se déplacer
    /// </summary>
    public override void SelectionnerPiece()
    {
        caseActuelle.SetEstActive(true);
        EstSelectionne = true;

        Vector2Int coordonneesDeCetteCase = new Vector2Int();
        Joueur.NumeroJoueur numeroJoueur = PlayersController.Instance._joueurActive.numeroJoueur;

        if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
        }
        else if (numeroJoueur is Joueur.NumeroJoueur.Joueur2)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
        }
        
        Piece pieceInNextCase; //Pièce qui va être retrouvé si jamais HasPieceOnCoord retourn vrai.

        for (int i = 0; i < moveSet.Length; i++)
        {
            _nextMove = moveSet[i];
            _nextMove += coordonneesDeCetteCase;
            BoardManager.Instance.ActiverCaseByCoord(_nextMove.x, _nextMove.y, true, numeroJoueur);

            /* Condition qui vérifie s'il est possible de manger une pièce 
            * (à améliorer pour vérifier si c'est la pièce de l'adversaire et  
            * non une des pièces du même joueur)
            */
            if (BoardManager.Instance.HasPieceOnCoord(_nextMove.x, _nextMove.y, out pieceInNextCase))
            {
                BoardManager.Instance.ActiverCaseByCoord(_nextMove.x, _nextMove.y, true, numeroJoueur);
                // Debug.Log("Test => Je peux manger cette pièce");
            }
        }
    }

    /// <summary>
    /// Méthode qui permet de déplacer un cheval
    /// </summary>
    /// <param name="caseDestination"></param>
    public override void DeplacerPiece(Case caseDestination)
    {
        _rb.MovePosition(caseDestination.transform.position);
    }

    /// <summary>
    /// Méthode qui permet de deseléctionner un cheval et désactive les cases
    /// où il peut se déplacer
    /// </summary>
    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;
        BoardManager.Instance.DesactiverCases();
    }
}