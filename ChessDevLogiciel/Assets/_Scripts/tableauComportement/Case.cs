using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    public string numeroDeCase;

    private Material _material;
    private Color _defaultColor;

    public bool EstActive
    {
        set
        {
            if (value)
            {
                _material.color = Color.red;
            }
            else
            {
                _material.color = _defaultColor;
            }
        }
        get
        {
            return EstActive;
        }
    }

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _defaultColor = _material.color;
    }

    /// <summary>
    /// Reférence à la pièce qui est dans la case pour pouvoir
    /// manipuler cette pièce après.
    /// </summary>
    [SerializeField] private Piece pieceDansLaCase;

    public void SetPieceDansLaCase(Piece nouvellePiece)
    {
        pieceDansLaCase = nouvellePiece;
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
