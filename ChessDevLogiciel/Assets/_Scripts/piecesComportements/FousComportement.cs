using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class FousComportement : Piece
{
    private Rigidbody _rb;

    private Vector2Int[] _moveSet = new Vector2Int[]
    {
      //new Vector2Int(0, 1), //Move normal du pion
      //new Vector2Int(0, 2), //Move au départ
     new Vector2Int(1, 1),  //move pour manger en diagonal droit
      new Vector2Int(-1, 1)  //move pour manger en diagonal gauche
                             //new Vector2Int(2, 1) //Exemple d'un mouvement en L (cheval)
                             //new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0) //Exemple de mouvement vers toute la droite
                             //new Vector2Int(-BoardManager.MAX_BOARD_SIZE, 0) //Exemple de mouvement vers toute la gauche
    };



    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        //On d�finit l'ensemble de mouvement de la pi�ce
        moveSet = _moveSet;
    }

    public override void SelectionnerPiece()
    {
        //On peut changer la couleur de la pi�ce icitte si l'on veut
        caseActuelle.SetEstActive(true); //On active la case o� se trouve cette pi�ce pour l'allumer
        EstSelectionne = true; //On marque la pi�ce comme s�l�ctionn�e

        /*
         * Il faut permettre seulement les d�placements possibles ici selon le type de pi�ce.
         * C'est le BoardManager qui activera les cases (pour que la pi�ce puisse se d�placer) selon le moveSet envoy�.
         *
         * Le moveSet pourra �tre modifi� avant d'activer les cases. Par exemple, le pion change son moveSet (ensemble de mouvements)
         * si une pi�ce se trouve dans un coin (car le pion mange en diagonale).
         * 
         * Dans ce cas, c'est un Pion, alors la logique du pion sera unique. Pourtant, vous pouvez vous baser sur cette
         * logique pour l'impl�menter dans les autres pi�ces.
         */

        Vector2Int coordonneesDeCetteCase = new Vector2Int();
        Joueur.NumeroJoueur numeroJoueur = PlayerController.Instance._joueurActive.numeroJoueur;
        //On va utiliser les coodonn�es d'une case relative au joueur. Si on ajoute plus de joueurs, le code reste flexible
        if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourBlanc;
        }
        else if (numeroJoueur is Joueur.NumeroJoueur.Joueur2)
        {
            coordonneesDeCetteCase = caseActuelle.coordonneesDeCasePourNoir;
        }

        
        /*
        //On check si on peut manger
        Vector2Int nextDiagonalMoveRight = moveSet[2]; //On utilise le move pour manger
        Vector2Int nextDiagonalMoveLeft = moveSet[3]; //On utilise le move pour manger
        nextDiagonalMoveRight += coordonneesDeCetteCase; //Pour conna�tre le mouvement r�latif � la position de cette case
        nextDiagonalMoveLeft += coordonneesDeCetteCase; //Pour conna�tre le mouvement r�latif � la position de cette case

        if (BoardManager.Instance.HasPieceOnCoord(nextDiagonalMoveRight.x, nextDiagonalMoveRight.y))
        {
            BoardManager.Instance.ActiverCaseByCoord(nextDiagonalMoveRight.x, nextDiagonalMoveRight.y, true, numeroJoueur);
        }

        if (BoardManager.Instance.HasPieceOnCoord(nextDiagonalMoveLeft.x, nextDiagonalMoveLeft.y))
        {
            BoardManager.Instance.ActiverCaseByCoord(nextDiagonalMoveLeft.x, nextDiagonalMoveLeft.y, true, numeroJoueur);
        }
        */
        //On active les cases d'un d�placement normal
        Vector2Int nextMove = moveSet[0] + coordonneesDeCetteCase;
        BoardManager.Instance.ActiverCaseByCoord(nextMove.x, nextMove.y, true, numeroJoueur);
        
        //C'EST TOUTE CETTE PARTIE qui change SELON la pi�ce
        for (int i = 0; i < moveSet.Length; i++)
        {
            Vector2Int nextMove1 = moveSet[0];
            nextMove1 += coordonneesDeCetteCase; //Pour conna�tre le mouvement r�latif � la position de cette case
                                                 //On active dans les coordonn�es y negatif
            for (int yPosi = coordonneesDeCetteCase.y; yPosi >= nextMove.y; yPosi--)
            {
                //On active les cases par coordonn�es dans le board
                BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, numeroJoueur);
                Debug.Log(nextMove.x + ":" + yPosi);

            }



        }

        /*
         //AUTRE EXEMPLE DE MOUVEMENT POUR UNE AUTRE PI�CE//
         //C'EST CETTE PARTIE qui change SELON la pi�ce
        //On rep�te pour tous les mouvements possibles dans _actualMoveSet
        for (int i = 0; i < moveSet.Length; i++)
        {
           Vector2Int nextMove = moveSet[i];
           nextMove += coordonneesDeCetteCase; //Pour conna�tre le mouvement r�latif � la position de cette case


           //On active dans les coordonn�es x positif
           for (int xPosi = coordonneesDeCetteCase.x; xPosi <= nextMove.x; xPosi++)
           {
              //On active les cases par coordonn�es dans le board
              BoardManager.Instance.ActiverCaseByCoord(xPosi, coordonneesDeCetteCase.y, true, numeroJoueur);
           }
           //On active dans les coordonn�es x negatif
           for (int xNega = coordonneesDeCetteCase.x; xNega >= nextMove.x; xNega--)
           {
              //On active les cases par coordonn�es dans le board
              BoardManager.Instance.ActiverCaseByCoord(xNega, coordonneesDeCetteCase.y, true, numeroJoueur);
           }

           //On active dans les coordonn�es y positif
           for (int yPosi = coordonneesDeCetteCase.y; yPosi <= nextMove.y; yPosi++)
           {
              //On active les cases par coordonn�es dans le board
              BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, numeroJoueur);
              Debug.Log(nextMove.x + ":" + yPosi);
           }
           //On active dans les coordonn�es y negatif
           for (int yPosi = coordonneesDeCetteCase.y; yPosi >= nextMove.y; yPosi--)
           {
              //On active les cases par coordonn�es dans le board
              BoardManager.Instance.ActiverCaseByCoord(nextMove.x, yPosi, true, numeroJoueur);
              Debug.Log(nextMove.x + ":" + yPosi);
           }
        }
        */
    }

    public override void DeplacerPiece(Case caseDestination)
    {
        //Dans le cas qu'il ait une pi�ce dans la case qu'on veut se d�placer,
        //on check si l'on peut la manger ou si c'est une de nos piece
        //pour ensuite se d�placer 



        //On d�place
        //On remplace la coordonn�e  y  pour qu'elle reste intacte
        Vector3 destination = caseDestination.transform.position;

        destination.y = transform.position.y;
        _rb.MovePosition(destination);

        //Finalemment,
        //On efface la r�ference de la pi�ce dans la case
        //et on ajoute la ref�rence de cette pi�ce � la case o� l'on se d�place
        //caseDestination.SetPieceDansLaCase(this);
    }

    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;

        //On dit au board de d�sactiver les cases actives
        BoardManager.Instance.DesactiverCases(); //le this est temporel
    }
}

