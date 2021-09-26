using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RookComportement : Piece
{
    private Rigidbody _rb;
    
    [SerializeField] private Vector2Int[] _moveSet = new Vector2Int[]
    {
        new Vector2Int(0, 2),
    };

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        //On définit l'ensemble de mouvement de la pièce
        moveSet = _moveSet;
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

    public override void SelectionnerPiece()
    {
        //On peut changer la couleur de la pièce si l'on veut
        EstSelectionne = true;
        
        //Il faut permettre seulement les déplacements possibles ici selon le type de pièce.
        //C'est le BoardManager qui activera les cases pour se déplacer selon le moveSet envoyé.
      
        //On affiche les cases possibles du pion pour se déplacer
    }

    public override void DeselectionnerPiece()
    {
        EstSelectionne = false;
    }

}
