using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;


/// <summary>
/// Classe qui teste le mouvement des chevaliers
/// </summary>
public class MouvementChevalierTests
{
    private GameObject _board;
    private GameObject _caseTableau;
    private GameObject _chevalier;
    private GameObject _gameManager;
    private GameObject _playerController;

    /// <summary>
    /// 
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _board = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board.prefab");
        _caseTableau = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        _chevalier = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Knight White.prefab");
        _gameManager = GameObject.Find("GameManager");
        _playerController = GameObject.Find("Players");
    }


    /// <summary>
    /// 
    /// </summary>
    [Test]
    public void VerifierCaseContientPiece()
    {
        Case caseTableau = _caseTableau.GetComponent<Case>();
        ChevalierComportement chevalier = _chevalier.GetComponent<ChevalierComportement>();

        caseTableau.SetPieceDansLaCase(chevalier);

        Assert.AreEqual(caseTableau.GetPieceDansLaCase(), chevalier);
    }

    [UnityTest]
    public IEnumerator DeplacementChevalier()
    {
        GameObject caseGo00 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        GameObject caseGo01 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        GameObject caseGo02 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        GameObject caseGo03 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        GameObject caseGo04 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        GameObject caseGo05 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        GameObject caseGo06 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        GameObject caseGo07 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");

        GameObject boardGo =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
        BoardManager boardManager = boardGo.GetComponent<BoardManager>();

        GameObject chevalierGo =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
        ChevalierComportement chevalier = chevalierGo.GetComponent<ChevalierComportement>();

        yield return null;

        //Mouvements possibles des chevaliers
        Case caseTableau00 = caseGo00.GetComponent<Case>();
        caseTableau00.coordonneesDeCasePourBlanc = new Vector2Int(1, 2);//Mouvement vers toute la droite
        Case caseTableau01 = caseGo01.GetComponent<Case>();
        caseTableau01.coordonneesDeCasePourBlanc = new Vector2Int(-1, 2);
        Case caseTableau02 = caseGo02.GetComponent<Case>();
        caseTableau02.coordonneesDeCasePourBlanc = new Vector2Int(2, -1);
        Case caseTableau03 = caseGo03.GetComponent<Case>();
        caseTableau03.coordonneesDeCasePourBlanc = new Vector2Int(-2, -1);
        Case caseTableau04 = caseGo04.GetComponent<Case>();
        caseTableau04.coordonneesDeCasePourBlanc = new Vector2Int(1, -2);
        Case caseTableau05 = caseGo05.GetComponent<Case>();
        caseTableau05.coordonneesDeCasePourBlanc = new Vector2Int(-1, -2);
        Case caseTableau06 = caseGo06.GetComponent<Case>();
        caseTableau06.coordonneesDeCasePourBlanc = new Vector2Int(2, 1);
        Case caseTableau07 = caseGo07.GetComponent<Case>();
        caseTableau07.coordonneesDeCasePourBlanc = new Vector2Int(-2, 1);
       
    }


    /// <summary>
    /// 
    /// </summary>
    [Test]
    public IEnumerator MouvementWithEnumeratorPasses()
    {
        yield return null;
    }
}
