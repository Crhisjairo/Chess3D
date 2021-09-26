using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { private set; get;}

    [SerializeField] private Case[] _cases;

    private Vector2Int[] _actualMoveSet;

    private void Awake()
    {
        //On évite avoir deux instance de cette même classe lors du Awake
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); //On destroy le gameObject qui contient ce script. Faire attention.
        }
        else
        {
            Instance = this;
        }
    }

    public void ActiverCasesRelativeTo(Piece piece)
    {
        piece.caseActuelle.EstActive = true;
        _actualMoveSet = piece.moveSet;
    }

    public void DesactiverCases(Piece piece)
    {
        piece.caseActuelle.EstActive = false;
    }
}
