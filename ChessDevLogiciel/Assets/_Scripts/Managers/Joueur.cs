using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script qui se charge de tous les composantes utilisés par le joueur
public class Joueur : MonoBehaviour
{
    //Initialisation de tous les variables dont nous allons avoir besoin dans le script
    private PlayerData _playerData;

    /// <summary>
    /// Numero du joueur
    /// </summary>
    public NumeroJoueur numeroJoueur;

    private List<Piece> _piecesMangees;

    //public Slider _timeSlider;
    public Text tempsRestantText;

    private float TempsRestant { get; set; } = 300f;

    private bool IsTimerOn { get; set; } = false;

    private bool EstTempsFinit { get; set; } = false;

    private float _startTime;
    private float _stopTime;


    [SerializeField] private Piece[] _piecesJoueur;
    private Coroutine _timerCoroutine;
   

    public void SetTimerOn(bool isOn)
    {
        IsTimerOn = isOn;
        
        if (IsTimerOn)
        {
           if (_timerCoroutine is null)
           {
               _timerCoroutine = StartCoroutine(TimerCountdown());
               _startTime = Time.time;
           } 
        }
        else
        {
            if (!(_timerCoroutine is null))
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
                TempsRestant = TempsRestant - (Time.time - _startTime);
            } 
        }
    }

    private IEnumerator TimerCountdown()
    {
        while (true)
        {
            float timerTime = TempsRestant - (Time.time - _startTime);

            if (timerTime <= 0)
            {
                EstTempsFinit = true;
                tempsRestantText.text = "Time out!";
                
                //TODO icitte on finit la game à cause du temps
                yield break;
            }

            string minutes = ((int) timerTime / 60).ToString();
            string secondes = Math.Truncate(timerTime % 60).ToString();

            tempsRestantText.text = minutes + ":" + secondes;

            yield return null;
        }
    }

    public void SetProperties(PlayerData playerData, float secondesPourJoueur)
    {
        if (playerData is null)
        {
            playerData = new PlayerData("999", "Invité", "", "", 0, 0, "");
        }
        
        _playerData = playerData;
                    
        //Si jamais c'est un invité
        if (_playerData.Username.Equals("Invité"))
        {
            // PlayerData.Nom + (int) numeroJoueur;
        }

        SetTempsRestant(secondesPourJoueur);
        
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

    public void SetTempsRestant(float temps)
    {
        TempsRestant = temps;
        
        string minutes = ((int) temps / 60).ToString();
        string secondes = Math.Truncate(temps % 60).ToString();

        tempsRestantText.text = minutes + ":" + secondes;
    }

    public void SetPieces(Piece[] pieces)
    {
        _piecesJoueur = pieces;
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
