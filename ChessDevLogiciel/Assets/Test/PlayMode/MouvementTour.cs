using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
/**
 * Classe qui test le comportement 
 */
    public class MouvementTour
    {
        //Initalisation de tous les variables que nous allons utiliser au long du code
        private GameObject board;
        private GameObject caseTableau;
        private GameObject _roque;

        [SetUp]
        //Methode dans laquels nous prenons les assets de tous de notre scene pour pouvoir les tester
        public void Setup()
        {
            board = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board.prefab");
            caseTableau = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
            _roque= AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
        }
        // Methode qui test si la piece est actuellement sur la piece 
        [Test]
        public void CaseContientPiece()
        {
           //On ajoute aux variables que nous avons initialiser, les comporsantes des scripts que nous voulons tester.
            Case _caseTableau = caseTableau.GetComponent<Case>();
            RoqueComportement roque = _roque.GetComponent<RoqueComportement>();


            _caseTableau.SetPieceDansLaCase(roque);
            //Assertation qui verifie si la case qui est dans la piece est la meme que celle de la variable roque que
            //nous avons initialiser           
            Assert.AreEqual(_caseTableau.GetPieceDansLaCase(), roque);
        }

        [UnityTest]    
        public IEnumerator DeplacementRoque()
        {
            GameObject caseGo00 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
            GameObject caseGo01 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
            GameObject caseGo02 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
            GameObject caseGo03 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");

            GameObject boardGo =
                AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
            BoardManager boardManager = boardGo.GetComponent<BoardManager>();
            
            GameObject roqueGo =
                AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
            RoqueComportement roqueComportement = roqueGo.GetComponent<RoqueComportement>(); 
            
            
            
            yield return null;
            
            
            
            
            //Mouvement possible pour la pieced de Roque
            Case caseTableau00 = caseGo00.GetComponent<Case>();
            caseTableau00.coordonneesDeCasePourBlanc = new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0);//Mouvement vers toute la droite
            Case caseTableau01 = caseGo01.GetComponent<Case>();
            caseTableau01.coordonneesDeCasePourBlanc = new Vector2Int(-BoardManager.MAX_BOARD_SIZE,0);//Mouvement vers toute la gauche
            Case caseTableau02 = caseGo02.GetComponent<Case>();
            caseTableau02.coordonneesDeCasePourBlanc = new Vector2Int(0,BoardManager.MAX_BOARD_SIZE);//Mouvement vers tous en haut
            Case caseTableau03 = caseGo03.GetComponent<Case>();
            caseTableau03.coordonneesDeCasePourBlanc = new Vector2Int(0, -BoardManager.MAX_BOARD_SIZE);//Mouvement vers tous en bas








        }
        
        
        

    }
