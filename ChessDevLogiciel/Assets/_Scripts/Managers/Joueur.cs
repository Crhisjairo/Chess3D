using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joueur : MonoBehaviour
{
    /// <summary>
    /// Numero du joueur
    /// </summary>
    public NumeroJoueur numeroJoueur;
    public string _nomJoueur = "";
    public float _tempsRestant;
    public float _tempsArrete;
    public bool _estArrete;
    public Transform camPosition;

    public Slider _timeSlider;
    public Text _tempsRestantText;
    public string _textTime;

    public string Nom { get; set; }
    public int Pointage { get; set; } = 0;
    public float TempsRestant { get; set; }
    public bool TempsArrete { get; set; }

    [SerializeField] private Piece[] _piecesJoueur;

    private void Start()
    {
        //Si jamais le nom est vide
        //if (_nomJoueur == string.Empty)
        //{
            _nomJoueur = "Joueur " + (int) numeroJoueur;
            _estArrete = false;
        //}
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
