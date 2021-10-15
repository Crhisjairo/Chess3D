using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetEstActive(bool estActive)
    {
        //On change la couleur de la case en fontion si elle va être activée ou pas.
        if (estActive)
        {
            if (pieceDansLaCase != null)
            {
                _material.color = Color.red;
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
