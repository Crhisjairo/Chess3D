using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class ChevalierComportement : Piece
{
    private Rigidbody _rb;

    private Vector2Int[] _moveSet = new Vector2Int[]
    {
      new Vector2Int(1, 2), // 1 pas vers la droite, 2 pas vers le haut (Mouvement de départ vers la droite)
      new Vector2Int(-1, 2), // 1 pas vers la gauche, 2 pas vers le haut (Mouvement de départ vers la gauche)
      new Vector2Int(2, -1), // 2 pas vers la droite, 1 pas vers le bas
      new Vector2Int(-2, -1), // 2 pas vers la gauche, 1 pas vers le bas
      new Vector2Int(1, -2), // 1 pas vers la droite, 2 pas vers le bas
      new Vector2Int(-1, -2), // 1 pas vers la gauche, 2 pas vers le bas
      new Vector2Int(2, 1), // 2 pas vers la droite, 1 pas vers le haut
      new Vector2Int(-2, 1) // 2 pas vers la gauche, 1 pas vers le haut
    };

    private bool isFirstMove;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
 
        //On définit l'ensemble de mouvement de la pièce
        moveSet = _moveSet;
        isFirstMove = true;
    }

    public override void SelectionnerPiece()
    {
        caseActuelle.SetEstActive(true);
        EstSelectionne = true;
        Debug.Log("Test case active"); /// BUGG

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

        if (isFirstMove)
        {
            Vector2Int firstMoveLeft = moveSet[0];
            firstMoveLeft = coordonneesDeCetteCase; 

            Vector2Int firstMoveRight = moveSet[1]; 
            firstMoveRight = coordonneesDeCetteCase; 

            BoardManager.Instance.ActiverCaseByCoord(firstMoveLeft.x, firstMoveLeft.y, true, numeroJoueur);
            BoardManager.Instance.ActiverCaseByCoord(firstMoveRight.x, firstMoveRight.y, true, numeroJoueur);
        }

        for (int i = 0; i < moveSet.Length; i++)
        {
            Vector2Int nextMove = moveSet[i];
            nextMove = coordonneesDeCetteCase;

            ///// Coordonnée x positif
            //for (int xPositif = coordonneesDeCetteCase.x; xPositif <= nextMove.x; xPositif++)
            //{
            //    if (BoardManager.Instance.HasPieceOnCoord(xPositif, nextMove.y) &&
            //        xPositif != coordonneesDeCetteCase.x)
            //    {
            //        break;
            //    }
            //    BoardManager.Instance.ActiverCaseByCoord(xPositif, coordonneesDeCetteCase.y, true, numeroJoueur);
            //}

            ///// Coordonnée x negatif
            //for (int xNegatif = coordonneesDeCetteCase.x; xNegatif >= nextMove.x; xNegatif--)
            //{
            //    if (BoardManager.Instance.HasPieceOnCoord(xNegatif, nextMove.y) &&
            //        xNegatif != coordonneesDeCetteCase.x)
            //    {
            //        break;
            //    }
            //    BoardManager.Instance.ActiverCaseByCoord(xNegatif, coordonneesDeCetteCase.y, true, numeroJoueur);
            //}

            ///// Coordonnée y positif
            //for (int yPositif = coordonneesDeCetteCase.y; yPositif <= nextMove.x; yPositif++)
            //{
            //    if (BoardManager.Instance.HasPieceOnCoord(coordonneesDeCetteCase.x, yPositif) &&
            //        yPositif != coordonneesDeCetteCase.x)
            //    {
            //        break;
            //    }
            //    BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPositif, true, numeroJoueur);
            //}

            ///// Coordonnée y negatif
            //for (int yNegatif = coordonneesDeCetteCase.x; yNegatif >= nextMove.x; yNegatif--)
            //{
            //    if (BoardManager.Instance.HasPieceOnCoord(coordonneesDeCetteCase.x, yNegatif) &&
            //        yNegatif != coordonneesDeCetteCase.x)
            //    {
            //        break;
            //    }
            //    BoardManager.Instance.ActiverCaseByCoord(coordonneesDeCetteCase.x, yNegatif, true, numeroJoueur);
            //}
        }
    }

    public override void DeplacerPiece(Case caseDestination)
    {
        Vector3 destination = caseDestination.transform.position;

        destination.x = transform.position.x;
        destination.y = transform.position.y;
        _rb.MovePosition(destination);

        if (isFirstMove)
        {
            isFirstMove = false;
        }
    }

    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;
        BoardManager.Instance.DesactiverCases();
    }
}
