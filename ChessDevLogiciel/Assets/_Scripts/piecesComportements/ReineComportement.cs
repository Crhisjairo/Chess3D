using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReineComportement : Piece
{

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
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false; //On cache le outline au début.

        moveSet = _moveSet;
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


        Vector2Int firstMove = moveSet[1];
        firstMove = coordonneesDeCetteCase;

        for (int y = coordonneesDeCetteCase.y; y <= firstMove.y; y++)
        {
            BoardManager.Instance.ActiverCaseByCoord(firstMove.x,y,true,numeroJoueur);
        }

    }
    
    public override void DeplacerPiece(Case caseDestination)
    {
        //Dans le cas qu'il ait une pièce dans la case qu'on veut se déplacer,
        //on check si l'on peut la manger ou si c'est une de nos piece
        //pour ensuite se déplacer 
      
        //On déplace
        //On remplace la coordonnée  y  pour qu'elle reste intacte
        Vector3 destination = caseDestination.transform.position;
      
        destination.y = transform.position.y;
        _rb.MovePosition(destination);
      
        //Finalemment,
        //On efface la réference de la pièce dans la case
        //et on ajoute la reférence de cette pièce à la case où l'on se déplace
        //caseDestination.SetPieceDansLaCase(this);
    }

    

    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;
      
        //On dit au board de désactiver les cases actives
        BoardManager.Instance.DesactiverCases(); //le this est temporel
    }

    
}
