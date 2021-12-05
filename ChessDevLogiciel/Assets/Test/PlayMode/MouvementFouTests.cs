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

    [SetUp]
    public void SetUp()
    {
        _caseTableau = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        _fou = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Bishop White.prefab");
    }


    [Test]
    public void CaseContientPiece()
    {
        Case caseTableau = _caseTableau.GetComponent<Case>();
        FousComportement fou = _fou.GetComponent<FousComportement>();

        caseTableau.SetPieceDansLaCase(fou);

        Assert.AreEqual(caseTableau.GetPieceDansLaCase(), fou);
    }

    [Test]
    public IEnumerator MouvementWithEnumeratorPasses()
    {
        yield return null;
    }
}
