using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe qui ex�cute le comportment du cheval et ses d�placements dans le jeu
/// </summary>
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class ChevalierComportement : Piece
{
    private Rigidbody _rb;

    // Liste de tous les d�placements possibles du chevalier
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

    private Vector2Int _nextMove; // Prochain d�placement du chevalier

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        moveSet = _moveSet; //On d�finit l'ensemble de mouvement de la pi�ce
    }

    /// <summary>
    /// M�thode qui permet de sel�ctionner un cheval et active les cases o� il
    /// peut se d�placer
    /// </summary>
    public override void SelectionnerPiece()
    {
        caseActuelle.SetEstActive(true);
        EstSelectionne = true;

        Vector2Int coordonneesDeCetteCase = new Vector2Int();
        Joueur.NumeroJoueur numeroJoueur = PlayerController.Instance._joueurActive.numeroJoueur;
        
        if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
        }
        else if (numeroJoueur is Joueur.NumeroJoueur.Joueur2)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
        }

        for (int i = 0; i < moveSet.Length; i++)
        {
            _nextMove = moveSet[i];
            _nextMove += coordonneesDeCetteCase;
            BoardManager.Instance.ActiverCaseByCoord(_nextMove.x, _nextMove.y, true, numeroJoueur);

            /// Condition qui v�rifie s'il est possible de manger une pi�ce 
            /// (� am�liorer pour v�rifier si c'est la pi�ce de l'adversaire et  
            /// non une des pi�ces du m�me joueur)
            if (BoardManager.Instance.HasPieceOnCoord(_nextMove.x, _nextMove.y))
            {
                BoardManager.Instance.ActiverCaseByCoord(_nextMove.x, _nextMove.y, true, numeroJoueur);
               // Debug.Log("Test => Je peux manger cette pi�ce");
            }
        }
    }

    /// <summary>
    /// M�thode qui permet de d�placer un cheval
    /// </summary>
    /// <param name="caseDestination"></param>
    public override void DeplacerPiece(Case caseDestination)
    {
        _rb.MovePosition(caseDestination.transform.position);
    }

    /// <summary>
    /// M�thode qui permet de desel�ctionner un cheval et d�sactive les cases
    /// o� il peut se d�placer
    /// </summary>
    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;
        BoardManager.Instance.DesactiverCases();
    }
}
