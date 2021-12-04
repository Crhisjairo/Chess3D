using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using _Scripts;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class MouvementPionTest
{
    /**
     * Explication:
     *
     * Pour tester des trucs punctuels, comme des variables qui changent dans nos scripts, ou des scripts qui n'ont pas
     * de dépendance (comme le ClockAnimation, par exemple), on peut juste créer une instance du prefab. Il faut donc
     * avoir une référence au préfab qu'on veut intantiate. La méthode OneTimeSetup() sauvegarde les références au Préfabs.
     * Chacun de ces tests doivent contenir la signature:
     * [Test]
     * public void CaseContientPieceApresAvoirSetLaPiece() {
     *
     * }
     *
     * 
     * Pour tester le jeu, je load la scène du Board. Tous les éléments sont là. Alors, la méthode Setup va toujours
     * recharge la même scène avant chaque test. Il faut recharger la scène avant chaque tests pour pas que les modifications
     * des tests influent aux autres test (dans le fond, t'as une scène board clean à chaque test, alors tu peux tout
     * modifier dans ta scène). On utilise ces genres de tests pour tester l'ensemble des comportements.
     * Chacun de ces tests doivent contenir la signature:
     *
     * [UnityTest]
     * public IEnumerator EstJoueurActivePremierJoueur(){
     *
     * }
     *
     */
    
    //Utiliser les prefabs si vous avez besoin de tester des choses simples
    private GameObject boardPrefab;
    private GameObject caseTableauPrefab;
    private GameObject pionPrefab;
    private GameObject playerControllerPrefab;
    private GameObject uiManagerPrefab;

    //Utiliser les games objects si vous avez besoin de tester toute la scène.
    private GameObject gameManagerGo;
    private GameObject playerControllerGo;
    private GameObject uIManagerGo;
    private GameObject boardGo;

    /**
     * Cette méthode s'execute une fois pendant toute la session de tests.
     */
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        boardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board.prefab");
        caseTableauPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        pionPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Pawn White.prefab");
        playerControllerPrefab =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Managers/Players.prefab");
        uiManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Managers/UIManager.prefab");
    }

    /**
     * Cette méthode s'execute avant chaque test.
     */
    [UnitySetUp]
    public IEnumerator Setup()
    {
        SceneManager.LoadSceneAsync("Board");
        
        var waitForScene = new WaitForSceneLoaded("Board");

        yield return waitForScene;
        
        gameManagerGo = GameObject.Find("GameManager");
        playerControllerGo = GameObject.Find("Players");
        uIManagerGo = GameObject.Find("UIManager");
        boardGo = GameObject.Find("Board");
    }

    [Test]
    public void CaseContientPieceApresAvoirSetLaPiece()
    {
        Case caseTab = caseTableauPrefab.GetComponent<Case>();
        PionComportement pionComportement = pionPrefab.GetComponent<PionComportement>();
        
        //On fait que le pion active la case
        caseTab.SetPieceDansLaCase(pionComportement);
        
        
        //On check si la case de cette case est activé si le pion se déplace là dedans
        Assert.AreEqual(caseTab.GetPieceDansLaCase(), pionComportement);
    }

    [UnityTest]
    public IEnumerator EstJoueurActivePremierJoueur()
    {
        GameManager gameManager = gameManagerGo.GetComponent<GameManager>();
        PlayersController playersController = playerControllerGo.GetComponent<PlayersController>();

        gameManager.OnClickStartGame();
        
        Assert.AreEqual(playersController._joueurActive.numeroJoueur, Joueur.NumeroJoueur.Joueur1);
        
        yield return null;
        
    }
    
    [UnityTest]
    public IEnumerator CaseActiveQuandPionLeDemande()
    {
        //TODO Il faut encore le continuer. Pas finit
        /*
        BoardManager boardManager = boardGo.GetComponent<BoardManager>();
        PlayersController playersController = playerControllerGo.GetComponent<PlayersController>();
        PionComportement pion = MonoBehaviour.Instantiate(pionPrefab).GetComponent<PionComportement>();
        Case caseTab00 = boardManager.GetCaseAtCoord(0, 0);
        Case caseTab01 = boardManager.GetCaseAtCoord(0, 1);
        Case caseTab02 = boardManager.GetCaseAtCoord(0, 2);
        
        playersController._joueurActive.SetPieces(new[] { pion});

        pion.caseActuelle = caseTab00;
        pion.SelectionnerPiece();

        
        Assert.AreEqual(caseTab00.EstActive(), true);
*/
        yield break;
        
    }
    
}
