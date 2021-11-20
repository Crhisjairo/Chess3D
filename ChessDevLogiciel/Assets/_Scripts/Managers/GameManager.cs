using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//Script qui va se charger du fonctionnement du jeu
public class GameManager : MonoBehaviour
{
    
    //Initialisation de tous les variables que nous aurons besoin dans ce script
    public static GameManager Instance { private set; get;}

    public GameMode gameMode;
    
    public PlayerData playerData;

    public Canvas gameSettings;
    public Slider sliderMinutes;
    public Slider sliderSecondes;
    
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
        
        DontDestroyOnLoad(this.gameObject);
    }
    
    private void Start()
    {
        

        gameSettings.enabled = false;
    }

    public void SetGameMode(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }

    public void SetLocalPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    public void OnInitGame()
    {
        //On affiche la fenetre de configuration
        gameSettings.enabled = true;
        
    }

    public void OnClickStartGame()
    {
        float seconds = sliderMinutes.value * 60 + sliderSecondes.value;

        if (gameMode is GameMode.VsPlayerLocal)
        {
            PlayersController.Instance.SetPlayersSettingsAndStartGame(playerData, seconds);
            
            //TODO il faut maintenant set la data dans le UI des players: Nom, temps, etc
        }
        
    }

    public enum GameMode
    {
        VsPlayerLocal,
        VsAI,
        VsPlayerOnline
    }
}
