using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script qui gère le comportement des pièces dans le Chess Board
public class Case : MonoBehaviour
{
    public string nomDeCase;
    public Vector2Int coordonneesDeCasePourBlanc;
    public Vector2Int coordonneesDeCasePourNoir;

    private Material _material;
    private Color _defaultColor;

    [SerializeField] private bool _estActive = false;
    
    /// <summary>
    /// Reférence à la pièce qui est dans la case pour pouvoir
    /// manipuler cette pièce après.
    /// </summary>
    [SerializeField] private Piece pieceDansLaCase;
    //Méthode Start() set up tous les componenent qui vont être utilisés
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _defaultColor = _material.color;
    }

    public void SetPieceDansLaCase(Piece nouvellePiece)
    {
        pieceDansLaCase = nouvellePiece;
    }

    public Piece GetPieceDansLaCase()
    {
        return pieceDansLaCase;
    }

    public bool EstActive()
    {
        return _estActive;
    }
//Méthode qui set la pièce active avec la couleur qui lui est fournis
    public void SetEstActive(bool estActive)
    {
        //On change la couleur de la case en fontion si elle va être activée ou pas.
        if (estActive)
        {
            if (pieceDansLaCase != null)
            {
                Debug.Log(pieceDansLaCase.EstSelectionne);
                
                if (!pieceDansLaCase.EstSelectionne)
                {
                    _material.color = Color.red;    
                }
                else
                {
                    //Si c'est la pièce qui est séléctionnée, la pièce qui va être à déplacer, on met green
                    _material.color = Color.green;
                }
                
            }
            else
            {
                _material.color = Color.cyan;
            }
        }
        else
        {
            _material.color = _defaultColor;
        }
        
        _estActive = estActive;
    }

    //SEULEMENT POUR DEBUG. FAUT EFFACER
    public bool HasPiece()
    {
        if (pieceDansLaCase != null)
        {
            return true;
        }

        return false;
    }
}
