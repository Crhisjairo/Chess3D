using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoqueComportement : Piece
{
    [SerializeField] private Vector2Int[] _moveSet = new Vector2Int[]
    {
        new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0), //Mouvement vers toute la droite
        new Vector2Int(-BoardManager.MAX_BOARD_SIZE, 0), //Mouvement vers toute la gauche
        new Vector2Int(0,BoardManager.MAX_BOARD_SIZE),//Mouvement vers tous en haut
        new Vector2Int(0,-BoardManager.MAX_BOARD_SIZE),//Mouvement vers tous en bas 
    };

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false; //On cache le outline au début.
        
        //On définit l'ensemble de mouvement de la pièce
        moveSet = _moveSet;
    }
    
    public override void SelectionnerPiece()
    {
        //On peut changer la couleur de la pièce si l'on veut
        EstSelectionne = true;
        caseActuelle.SetEstActive(true);

        //Il faut permettre seulement les déplacements possibles ici selon le type de pièce.
        //C'est le BoardManager qui activera les cases pour se déplacer selon le moveSet envoyé.

        Vector2Int coordonneesDeCetteCase = new Vector2Int();
        Joueur.NumeroJoueur joueurActuel = PlayersController.Instance._joueurActive.numeroJoueur;


        if (joueurActuel is Joueur.NumeroJoueur.Joueur1)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
        }
        else if(joueurActuel is Joueur.NumeroJoueur.Joueur2)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
        }
        

        Piece pieceInNextCase; //Pièce qui va être retrouvé si jamais HasPieceOnCoord retourn vrai.

        //On affiche les cases possibles de la tour pour se déplacer
        for (int i = 0; i < moveSet.Length; i++)
        {
            Vector2Int nextMove = moveSet[i];
            nextMove += coordonneesDeCetteCase; //Pour connaître le mouvement rélatif à la position de cette case
            
            //On active dans les coordonnées x positif
            for (int xPosi = coordonneesDeCetteCase.x + 1; xPosi <= nextMove.x; xPosi++)
            {
                if (BoardManager.Instance.HasPieceOnCoord(xPosi, nextMove.y, out pieceInNextCase))
                {
                    //S'il s'agit d'un ennemie
                    if (pieceInNextCase.JoueurProprietaire != joueurActuel)
                    {
                        //On active les cases par coordonn�es dans le board
                        BoardManager.Instance.ActiverCaseByCoord(xPosi, nextMove.y, true, joueurActuel);
                        break;
                    }
                    else //S'il s'agit de nous même, on s'arrête
                    {
                        break;
                    }
                }
                else
                {
                    //On active les cases par coordonn�es dans le board
                    BoardManager.Instance.ActiverCaseByCoord(xPosi, nextMove.y, true, joueurActuel);
                }
                
            }
            
            //On active dans les coordonnées x negatif
            for (int xNega = coordonneesDeCetteCase.x - 1; xNega >= nextMove.x; xNega--)
            {

                if (BoardManager.Instance.HasPieceOnCoord(xNega, nextMove.y, out pieceInNextCase))
                {
                    //S'il s'agit d'un ennemie
                    if (pieceInNextCase.JoueurProprietaire != joueurActuel)
                    {
                        //On active les cases par coordonn�es dans le board
                        BoardManager.Instance.ActiverCaseByCoord(xNega, nextMove.y, true, joueurActuel);
                        break;
                    }
                    else //S'il s'agit de nous même, on s'arrête
                    {
                        break;
                    }
                }
                else
                {
                    //On active les cases par coordonn�es dans le board
                    BoardManager.Instance.ActiverCaseByCoord(xNega, nextMove.y, true, joueurActuel);
                }
            }

            //On active dans les coordonnées y positif
            for (int yPosi = coordonneesDeCetteCase.y + 1; yPosi <= nextMove.y; yPosi++)
            {
                Debug.Log(yPosi);
                
                if (BoardManager.Instance.HasPieceOnCoord(coordonneesDeCetteCase.x, yPosi, out pieceInNextCase))
                {
                    //S'il s'agit d'un ennemie
                    if (pieceInNextCase.JoueurProprietaire != joueurActuel)
                    {
                        //On active les cases par coordonn�es dans le board
                        BoardManager.Instance.ActiverCaseByCoord(coordonneesDeCetteCase.x, yPosi, true, joueurActuel);
                        break;
                    }
                    else //S'il s'agit de nous même, on s'arrête
                    {
                        break;
                    }
                }
                else
                {
                    //On active les cases par coordonn�es dans le board
                    BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, joueurActuel);
                }
            }
            //On active dans les coordonnées y negatif
            for (int yPosi = coordonneesDeCetteCase.y - 1; yPosi >= nextMove.y; yPosi--)
            {
                if (BoardManager.Instance.HasPieceOnCoord(coordonneesDeCetteCase.x, yPosi, out pieceInNextCase))
                {
                    //S'il s'agit d'un ennemie
                    if (pieceInNextCase.JoueurProprietaire != joueurActuel)
                    {
                        //On active les cases par coordonn�es dans le board
                        BoardManager.Instance.ActiverCaseByCoord(coordonneesDeCetteCase.x, yPosi, true, joueurActuel);
                        break;
                    }
                    else //S'il s'agit de nous même, on s'arrête
                    {
                        break;
                    }
                }
                else
                {
                    //On active les cases par coordonn�es dans le board
                    BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, joueurActuel);
                }
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
        
    }

    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;
        
        BoardManager.Instance.DesactiverCases();
        
        
    }

}
