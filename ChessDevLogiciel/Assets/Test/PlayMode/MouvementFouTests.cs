using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;



public class MouvementFouTests
{
    private GameObject _caseTableau;
    private GameObject _fou;
    private GameObject _board;
    private GameObject _gameManager;
    private GameObject _playerController;

    [SetUp]
    public void SetUp()
    {
        _caseTableau = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        _fou = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Bishop White.prefab");
        _board = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board.prefab");
        _caseTableau = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        _gameManager = GameObject.Find("GameManager");
        _playerController = GameObject.Find("Players");
    }


    [Test]
    public void VerifierCaseContientPiece()
    {
        Case caseTableau = _caseTableau.GetComponent<Case>();
        FousComportement fou = _fou.GetComponent<FousComportement>();

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

        GameObject boardGo =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Bishop White.prefab");
        BoardManager boardManager = boardGo.GetComponent<BoardManager>();

        GameObject fouGo =
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Bishop White.prefab");
        FousComportement fou = fouGo.GetComponent<FousComportement>();

        yield return null;

        //Mouvements possibles d'un fou
        Case caseTableau00 = caseGo00.GetComponent<Case>();
        caseTableau00.coordonneesDeCasePourBlanc = new Vector2Int(1, 1); //move pour manger en diagonal droit vers le haut
        Case caseTableau01 = caseGo01.GetComponent<Case>();
        caseTableau01.coordonneesDeCasePourBlanc = new Vector2Int(-1, 1); //move pour manger en diagonal gauche vers le haut
        Case caseTableau02 = caseGo02.GetComponent<Case>();
        caseTableau02.coordonneesDeCasePourBlanc = new Vector2Int(1, -1); //move pour manger en diagonal droite vers le bas
        Case caseTableau03 = caseGo03.GetComponent<Case>();
        caseTableau03.coordonneesDeCasePourBlanc = new Vector2Int(-1, -1); //move pour manger en diagonal gauche vers le bas
    }

}
