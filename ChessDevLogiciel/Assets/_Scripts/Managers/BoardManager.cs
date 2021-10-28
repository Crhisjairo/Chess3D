using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    /**
   * Dans cette partie du code, on initialise les variables qui vont être utilisés dans le script du BoardManager
   */
    public static BoardManager Instance { private set; get;}
    public const int MAX_BOARD_SIZE = 8;
    private Case[] _cases;
    

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

    private void Start()
    {
        DesactiverCases();
    }

    /// <summary>
    /// Active ou désactive une case selon les coordonnées et le numéro de joueur envoyés.
    /// À OPTIMISER, méthode faite juste pour pas nous donner du retard xd.
    /// </summary>
    /// <param name="x">Coordonnée X de la case</param>
    /// <param name="y">Coordonnée Y de la case</param>
    /// <param name="activer">Si la case doit être activée ou pas</param>
    /// <param name="numeroJoueur">Pour</param>
    public void ActiverCaseByCoord(int x, int y, bool activer, Joueur.NumeroJoueur numeroJoueur)
    {
        foreach (Case uneCase in _cases)
        {
            Vector2Int coordonneesDeCase = new Vector2Int();
            
            if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
            {
                coordonneesDeCase = uneCase.coordonneesDeCasePourBlanc;
            } else if (numeroJoueur is Joueur.NumeroJoueur.Joueur2)
            {
                coordonneesDeCase = uneCase.coordonneesDeCasePourNoir;
            }
            
            //On check que les coordonnées matchent et qu'on active pas une case qui contient la pièce du même joueur.
            if (coordonneesDeCase == new Vector2Int(x, y))
            {
                uneCase.SetEstActive(activer);
            }
        }
    }

    /// <summary>
    /// Returne une pièce si la case contient un pièce selon les coordonnées.
    /// À OPTIMISER AUSSI, méthode faite juste pour pas nous donner du retard xd.
    /// </summary>
    /// <param name="x">Coordonnée X de la case</param>
    /// <param name="y">Coordonnée Y de la case</param>
    /// <returns>S'il existe une pièce dans la case</returns>
    public bool HasPieceOnCoord(int x, int y, out Piece pieceDansCase)
    {
        Joueur.NumeroJoueur numeroJoueur = PlayersController.Instance._joueurActive.numeroJoueur;
        
        foreach (Case uneCase in _cases)
        {
            Vector2Int coordonneesDeCase = new Vector2Int();
            
            if (numeroJoueur is Joueur.NumeroJoueur.Joueur1)
            {
                coordonneesDeCase = uneCase.coordonneesDeCasePourBlanc;
            } else if (numeroJoueur is Joueur.NumeroJoueur.Joueur2)
            {
                coordonneesDeCase = uneCase.coordonneesDeCasePourNoir;
            }
            
            if (coordonneesDeCase == new Vector2Int(x, y) && uneCase.HasPiece())
            {
                pieceDansCase = uneCase.GetPieceDansLaCase();
                return true;
            }
        }

        
        pieceDansCase = null;
        return false;
    }
    /**
     *  Code qui désactive les cases, lorsqu'une pièce n'est pas sélectionner. Cette méthode est en liaison avec
     *  chaque script de comportement des pièces
     */
    public void DesactiverCases()
    {
        //Au début, on désactive toutes les cases
        foreach (Case uneCase in _cases)
        {
            uneCase.SetEstActive(false);
        }
        
    }

}
