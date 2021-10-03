using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    /// <summary>
    /// Numero du joueur
    /// </summary>
    public NumeroJoueur numeroJoueur;

    public Transform camPosition;

    public string Nom { get; set; }
    public int Pointage { get; set; } = 0;
    
    [SerializeField] private Piece[] _piecesJoueur;

    private void Start()
    {
        //Si jamais le nom est vide
        if (Nom == string.Empty)
        {
            Nom = "Joueur " + (int) numeroJoueur;
        }
    }

    public void SetPiecesActives(bool sontPiecesActives)
    {
        //À remplacer par un event system
        foreach (Piece piece in _piecesJoueur)
        {
            piece.EstActive = sontPiecesActives;
        }
    }

    /// <summary>
    /// Sa représente les numero des joueurs.
    /// Le premier joueur ayant la valeur 0.
    /// </summary>
    public enum NumeroJoueur
    {
        Joueur1 = 1,
        Joueur2 = 2,
        Joueur3 = 3,
        Joueur4 = 4
    }
}
