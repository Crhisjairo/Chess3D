using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get;}

    [SerializeField] private Animator _drivenCamAnimator;

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

    public void ChangerCameraTo(Joueur.NumeroJoueur numeroJoueur)
    {
        //Faut changer la camera par Cinemachine Virtual Camera
        if (numeroJoueur == Joueur.NumeroJoueur.Joueur1)
        {
            Debug.Log("Icitte");
            _drivenCamAnimator.Play("vcamPlayer1");
        } 
        else if (numeroJoueur == Joueur.NumeroJoueur.Joueur2)
        {
            _drivenCamAnimator.Play("vcamPlayer2");
        } //On peut ajouter d'autres caméras pour d'autres joueurs. Il faudra adapter l'animator et le stateDrivenCam
    }
}
