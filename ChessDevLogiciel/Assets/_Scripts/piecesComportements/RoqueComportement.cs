using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RookComportement : MonoBehaviour, IPiece
{
    private Rigidbody _rb;
    private bool _isSelected;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void DeplacerPiece(Case caseDestination)
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
        
        //Pour terminer, il faut ajouter la reference de cette piece a la case ou nous la deplacons
        caseDestination.SetPieceDansLaCase(this.gameObject);
    }

    public bool PeutEtreSelectionne()
    {
        throw new NotImplementedException();
    }

    public bool EstSelectionne()
    {
        // On affiche les cases possibles au roque pour se deplacer
        //Il faudra se communiquer avec les cases du tableau
        return _isSelected;
    }

    public void SelectionnerPiece()
    {
        //On cache les cases disponibles
        //Il faudra commiquer avec les cases du tableau
        _isSelected = true;
    }

    public void DeselectionnerPiece()
    {
        _isSelected = false;
    }
}
