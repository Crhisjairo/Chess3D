using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RoqueComportement : Piece
{
    private Rigidbody _rb;

    private bool isFirstmove;
    
    [SerializeField] private Vector2Int[] _moveSet = new Vector2Int[]
    {
        new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0), //Exemple de mouvement vers toute la droite
        new Vector2Int(-BoardManager.MAX_BOARD_SIZE, 0), //Exemple de mouvement vers toute la gauche
        new Vector2Int(0,BoardManager.MAX_BOARD_SIZE),
        new Vector2Int(0,-BoardManager.MAX_BOARD_SIZE),
        new Vector2Int(BoardManager.MAX_BOARD_SIZE,-BoardManager.MAX_BOARD_SIZE)



    };

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        //On définit l'ensemble de mouvement de la pièce
        moveSet = _moveSet;
        isFirstmove = true;
    }
    
    public override void SelectionnerPiece()
    {
        //On peut changer la couleur de la pièce si l'on veut
        caseActuelle.SetEstActive(true);
        EstSelectionne = true;
        
        //Il faut permettre seulement les déplacements possibles ici selon le type de pièce.
        //C'est le BoardManager qui activera les cases pour se déplacer selon le moveSet envoyé.

        Vector2Int coordonneesDeCetteCase = new Vector2Int();
        Joueur.NumeroJoueur numeroJoueur = PlayerController.Instance._joueurActive.numeroJoueur;


        if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
        }
        else if(numeroJoueur is Joueur.NumeroJoueur.Joueur2)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
        }


        if (isFirstmove)
        {
            Vector2Int firstMove = moveSet[1];
            firstMove = coordonneesDeCetteCase;

            for (int y = coordonneesDeCetteCase.y; y <= firstMove.y; y++)
            {
                BoardManager.Instance.ActiverCaseByCoord(firstMove.x,y,true,numeroJoueur);
            }
        }



        //On affiche les cases possibles de la tour pour se déplacer
        for (int i = 0; i < moveSet.Length; i++)
        {
            Vector2Int nextMove = moveSet[i];
            nextMove += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case
         
         
            //On active dans les coordonnées x positif
            for (int xPosi = coordonneesDeCetteCase.x; xPosi <= nextMove.x; xPosi++)
            {
                if (BoardManager.Instance.HasPieceOnCoord(nextMove.x, nextMove.y))
                {
                    break;
                }
                //On active les cases par coordonnées dans le board
                BoardManager.Instance.ActiverCaseByCoord(xPosi, coordonneesDeCetteCase.y, true, numeroJoueur);
            }
            //On active dans les coordonnées x negatif
            for (int xNega = coordonneesDeCetteCase.x; xNega >= nextMove.x; xNega--)
            {
                if (BoardManager.Instance.HasPieceOnCoord(nextMove.x, nextMove.y))
                {
                    break;
                }
                //On active les cases par coordonnées dans le board
                BoardManager.Instance.ActiverCaseByCoord(xNega, coordonneesDeCetteCase.y, true, numeroJoueur);
            }

            //On active dans les coordonnées y positif
            for (int yPosi = coordonneesDeCetteCase.y; yPosi <= nextMove.y; yPosi++)
            {
                if (BoardManager.Instance.HasPieceOnCoord(nextMove.x, nextMove.y))
                {
                    break;
                }
                //On active les cases par coordonnées dans le board
                BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, numeroJoueur);
                Debug.Log(nextMove.x + ":" + yPosi);
            }
            //On active dans les coordonnées y negatif
            for (int yPosi = coordonneesDeCetteCase.y; yPosi >= nextMove.y; yPosi--)
            {
                if (BoardManager.Instance.HasPieceOnCoord(nextMove.x, nextMove.y))
                {
                    break;
                }
                //On active les cases par coordonnées dans le board
                BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, numeroJoueur);
                Debug.Log(nextMove.x + ":" + yPosi);
            }
        }
        






    }
    
    public override void DeplacerPiece(Case caseDestination)
    {
        //Il faut permettre seulement les deplacements possibles selon le type de piece
        //Dans ce cas, la piece va etre le roque
        
        
        
        
        
        
        //S'il y a une piece dans la case ou nous voulons nous deplacer,
        //on regarde si c'est une piece ennemi pour la manger ou c'est une de nos piece
        //pour choisir une autre destination
        
        
        
        //On deplace la roque
        //On remplace la coordonne y pour qu'elle reste intacte
        Vector3 destination = caseDestination.transform.position;

        destination.y = transform.position.y;
        _rb.MovePosition(destination);

        if (isFirstmove)
        {
            isFirstmove = false;
        }
        
    }

    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;
        
        BoardManager.Instance.DesactiverCases();
        
        
    }

}
