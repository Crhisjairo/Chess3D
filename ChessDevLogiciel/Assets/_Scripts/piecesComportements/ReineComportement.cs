using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReineComportement : Piece
{
    private Vector2Int[] _moveSetTour = new[]
    {
        //Mouvement de tour
        new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0), //Mouvement vers toute la droite [0]
        new Vector2Int(-BoardManager.MAX_BOARD_SIZE, 0), //Mouvement vers toute la gauche [1]
        new Vector2Int(0, BoardManager.MAX_BOARD_SIZE), //Mouvement vers tous en haut [2]
        new Vector2Int(0, -BoardManager.MAX_BOARD_SIZE), //Mouvement vers tous en bas [3]
    };

    private Vector2Int[] _moveSetFou = new[]
    {
        //Mouvement de fou
        new Vector2Int(1, 1), //move pour manger en diagonal droit vers le haut
        new Vector2Int(-1, 1), //move pour manger en diagonal gauche vers le haut
        new Vector2Int(1, -1), //move pour manger en diagonal droite vers le bas
        new Vector2Int(-1, -1) //move pour manger en diagonal gauche vers le bas
    };


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false; //On cache le outline au début.

        moveSet = _moveSetFou.Concat(_moveSetTour).ToArray();
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
        else if (joueurActuel is Joueur.NumeroJoueur.Joueur2)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
        }


        // Mouvement de la tour
        Piece pieceInNextCase; //Pièce qui va être retrouvé si jamais HasPieceOnCoord retourn vrai.

        for (int i = 0; i < _moveSetTour.Length; i++) //Car a 4 sa finit le mouve de la tour
        {
            Vector2Int nextMove = _moveSetTour[i];
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

        // Mouvement fou
        for (int moveSetIndex = 0;
            moveSetIndex < _moveSetFou.Length;
            moveSetIndex++) //Car apartir du 4 c'est le move du fou
        {
            Vector2Int nextMove = _moveSetFou[moveSetIndex];
            nextMove += coordonneesDeCetteCase; //Pour conna�tre le mouvement r�latif � la position de cette case

            //Pour la diagonal ver le haut droit
            for (int yPosi = coordonneesDeCetteCase.y; yPosi <= BoardManager.MAX_BOARD_SIZE; yPosi++)
            {
                //Debug.Log(nextMove);

                if (BoardManager.Instance.HasPieceOnCoord(nextMove.x, nextMove.y, out pieceInNextCase))
                {
                    //S'il s'agit d'un ennemie
                    if (pieceInNextCase.JoueurProprietaire != joueurActuel)
                    {
                        //On active les cases par coordonn�es dans le board
                        BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, joueurActuel);
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
                    BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, joueurActuel);
                }

                nextMove += _moveSetFou[moveSetIndex];
            }
        }
    }

    public override void DeplacerPiece(Case caseDestination)
    {
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