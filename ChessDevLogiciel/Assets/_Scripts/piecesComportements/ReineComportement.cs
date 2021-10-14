using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class ReineComportement : Piece
{
    private Rigidbody _rb;

    private bool isFirstmove;

    [SerializeField] private Vector2Int[] _moveSet = new Vector2Int[]
    {
        new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0), //Mouvement vers toute la droite
        new Vector2Int(-BoardManager.MAX_BOARD_SIZE, 0), //Mouvement vers toute la gauche
        new Vector2Int(0,BoardManager.MAX_BOARD_SIZE),//Mouvement vers tous en haut
        new Vector2Int(0,-BoardManager.MAX_BOARD_SIZE),//Mouvement vers tous en bas
        new Vector2Int(0, 1), //Move vers en avant de 1 case
        new Vector2Int(0, 2), //Move vers en avant de 2 case
    };
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

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
        Joueur.NumeroJoueur numeroJoueur = PlayersController.Instance._joueurActive.numeroJoueur;


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

    }
    



// Update is called once per frame
    public override void DeplacerPiece(Case caseDestination)
    {
        
    }

    

    public override void DeselectionnerPiece()
    {
        
    }

    
}
