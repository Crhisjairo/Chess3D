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

    private List<Piece> _piecesMangees;

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
        
        _piecesMangees = new List<Piece>();
        
        SetProprietairePieces(); //On se donne comme proprietaire de ces pièces
    }

    /**
     * On donne le numéro de joueur auquel les pièces appartiennent.
     */
    private void SetProprietairePieces()
    {
        foreach (Piece piece in _piecesJoueur)
        {
            piece.SetPieceProprietaire(numeroJoueur);
        }
    }

    public void SetPiecesActives(bool sontPiecesActives)
    {
        //À remplacer par un event system
        foreach (Piece piece in _piecesJoueur)
        {
            piece.SetEstActive(sontPiecesActives);
        }
    }

    
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
