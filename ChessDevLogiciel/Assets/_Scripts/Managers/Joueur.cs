using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script qui se charge de tous les composantes utilisés par le joueur
public class Joueur : MonoBehaviour
{
    //Initialisation de tous les variables dont nous allons avoir besoin dans le script
    private PlayerData PlayerData { get; set; }

    /// <summary>
    /// Numero du joueur
    /// </summary>
    public NumeroJoueur numeroJoueur;

    private List<Piece> _piecesMangees;

    //public Slider _timeSlider;
    public Text _tempsRestantText;

    public float TempsRestant { get; set; } = 300f;
    public bool EstTempsArrete { get; set; } = false;

    public bool EstTempsFinit { get; private set; } = false;


    [SerializeField] private Piece[] _piecesJoueur;

    private void Start()
    {
        //On donne des données par défaut pour les invités
        if (PlayerData is null)
        {
            PlayerData = new PlayerData("NoID", "Invité", "NoUsername", "", 0, 0);
        }
        //Si jamais c'est un invité
        if (PlayerData.Nom.Equals("Invité"))
        {
           // PlayerData.Nom + (int) numeroJoueur;
        }
        
        _piecesMangees = new List<Piece>();
        
        SetProprietairePieces(); //On se donne comme proprietaire de ces pièces
    }

    private void Update()
    {
        //On check si le temps est arreté pour ne pas continuer avec le timer.
        if (!EstTempsArrete)
        {
          TimerCountdown();  
        }
        
    }

    private void TimerCountdown()
    {
        float temps = TempsRestant - Time.time;

        if (temps <= 0)
        {
            EstTempsFinit = true;
            _tempsRestantText.text = "Time out!";
            return;
        }

        string minutes = ((int) temps / 60).ToString();
        string secondes = Math.Truncate(temps % 60).ToString();

        _tempsRestantText.text = minutes + ":" + secondes;
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
