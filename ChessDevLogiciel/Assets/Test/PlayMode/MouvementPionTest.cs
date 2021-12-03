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
        //Il faut encore le continuer
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

        yield break;
        
    }
    
    /// <summary>
    /// Utilisation de reflection pour acceder à une valeur d'un objet.
    /// </summary>
    ///
    /// <param name="type">Type d'instance</param>
    /// <param name="instance">Objet d'instance</param>
    /// <param name="fieldName">Le nom de l'attribut qu'on veut acceder</param>
    ///
    /// <returns>The field value from the object.</returns>
    internal static object GetInstanceField(Type type, object instance, string fieldName)
    {
        BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                 | BindingFlags.Static;
        FieldInfo field = type.GetField(fieldName, bindFlags);
        return field.GetValue(instance);
    }
    
}
