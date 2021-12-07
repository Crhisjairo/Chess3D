using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;



public class MouvementRoiTests
{
    private GameObject _caseTableau;
    private GameObject _roi;
    private GameObject _board;
    private GameObject _gameManager;
    private GameObject _playerController;

    [SetUp]
    public void SetUp()
    {
        _caseTableau = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        _roi = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess King White.prefab");
        _board = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board.prefab");
        _caseTableau = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        _gameManager = GameObject.Find("GameManager");
        _playerController = GameObject.Find("Players");
    }


    [Test]
    public void VerifierCaseContientPiece()
    {
        Case caseTableau = _caseTableau.GetComponent<Case>();
        RoiComportement fou = _roi.GetComponent<RoiComportement>();

        caseTableau.SetPieceDansLaCase(fou);

        Assert.AreEqual(caseTableau.GetPieceDansLaCase(), fou);
    }

    [UnityTest]
    public IEnumerator DeplacementFou()
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
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess King White.prefab");
        BoardManager boardManager = boardGo.GetComponent<BoardManager>();

        GameObject roiGo =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess King White.prefab");
        RoiComportement roi = roiGo.GetComponent<RoiComportement>();

        yield return null;

        //Mouvements possibles d'un roi
        Case caseTableau00 = caseGo00.GetComponent<Case>();
        caseTableau00.coordonneesDeCasePourBlanc = new Vector2Int(0, 1); //Move au départ
        Case caseTableau01 = caseGo01.GetComponent<Case>();
        caseTableau01.coordonneesDeCasePourBlanc = new Vector2Int(1, 1); //move pour manger en diagonal droit
        Case caseTableau02 = caseGo02.GetComponent<Case>();
        caseTableau02.coordonneesDeCasePourBlanc = new Vector2Int(-1, -1); //move pour manger en diagonal en bas droite
        Case caseTableau03 = caseGo03.GetComponent<Case>();
        caseTableau03.coordonneesDeCasePourBlanc = new Vector2Int(-1, 1); //move pour manger en diagonal gauche
        Case caseTableau04 = caseGo03.GetComponent<Case>();
        caseTableau04.coordonneesDeCasePourBlanc = new Vector2Int(1, -1); //move pour manger en diagonal en bas gauche
        Case caseTableau05 = caseGo03.GetComponent<Case>();
        caseTableau05.coordonneesDeCasePourBlanc = new Vector2Int(0, -1); //move pour diagonale derriere
        Case caseTableau06 = caseGo03.GetComponent<Case>();
        caseTableau06.coordonneesDeCasePourBlanc = new Vector2Int(1, 0); 
    }

}
