using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MouvementPionTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void CaseEstActiveQuandPionLeDemande()
    {
        //On crée un gameObject avec notre comportement d'une case
        var caseTableau = new GameObject();
        caseTableau.AddComponent<Case>();
        //On crée un pion
        var pion = new GameObject();
        pion.AddComponent<PionComportement>();
        
        //On fait que le pion active la case
        
        
        
        //On check si la case de cette case est activé si le pion se déplace là dedans
        Assert.AreEqual(caseTableau.GetComponent<Case>().EstActive(), true);
    }
    
    


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MouvemWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
