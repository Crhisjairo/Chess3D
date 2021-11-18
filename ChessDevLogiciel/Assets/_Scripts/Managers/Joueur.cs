using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script qui se charge de tous les composantes utilisés par le joueur
public class Joueur : MonoBehaviour
{
    //Initialisation de tous les variables dont nous allons avoir besoin dans le script
    /// <summary>
    /// Numero du joueur
    /// </summary>
    public NumeroJoueur numeroJoueur;

    private List<Piece> _piecesMangees;

    //public Slider _timeSlider;
    public Text _tempsRestantText;
    public string _textTime;

    public string Nom { get; set; }
    public int Pointage { get; set; } = 0;
    public float TempsRestant { get; set; } = 300f;
    public bool TempsEstArrete { get; set; } = false;

    [SerializeField] private Piece[] _piecesJoueur;

    private void Start()
    {
        //Si jamais le nom est vide
        if (Nom == string.Empty)
        {
            Nom = "Joueur " + (int) numeroJoueur;
        }
        
        _piecesMangees = new List<Piece>();
        
        SetProprietairePieces(); //On se donne comme proprietaire de ces pièces
    }

    /**
     * On donne le numéro de joueur aux pièces auquels elles appartiennent.
     */
    private void SetProprietairePieces()
    {
        foreach (Piece piece in _piecesJoueur)
        {
            piece.JoueurProprietaire = numeroJoueur;
        }
    }
//On mets les pièces actives pour les joueurs
    public void SetPiecesActives(bool sontPiecesActives)
    {
        //À remplacer par un event system
        foreach (Piece piece in _piecesJoueur)
        {
            piece.SetEstActive(sontPiecesActives);
        }
    }

    //On ajoute les pièces qui ont été mangés dans une liste
    public void AjouterPieceMangee(Piece piece)
    {
        //On désactive la pièce.
        
        _piecesMangees.Add(piece);
    }

    public void EnleverPieceMangee(Piece piece)
    {
        
    }

    /// <summary>
    /// Récupère les pièces qui ont été mangées par le joueur.
    /// </summary>
    /// <returns>Liste de pièces mangées par le joueur.</returns>
    public List<Piece> GetPiecesMangees()
    {
        return _piecesMangees;
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
