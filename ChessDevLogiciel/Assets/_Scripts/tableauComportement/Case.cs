using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    /// <summary>
    /// Reférence à la pièce qui est dans la case pour pouvoir
    /// manipuler cette pièce après.
    /// </summary>
    private GameObject pieceDansLaCase;

    public void SetPieceDansLaCase(GameObject nouvellePiece)
    {
        pieceDansLaCase = nouvellePiece;
    }

}
