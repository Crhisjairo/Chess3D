using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
/**
 * Classe qui test le comportement 
 */
    public class MouvementTour
    {
        //Initalisation de tous les variables que nous allons utiliser au long du code
        private GameObject boardPrefab;
        private GameObject caseTableauPrefab;
        private GameObject roquePrefab;

        //Initialisation des gameObject dont nous avons besoin pour tester ce code
        private GameObject gameManagerGo;
        private GameObject playerControllerGo;
        private GameObject uIManagerGo;
        private GameObject boardGo;

        [OneTimeSetUp]
        //Methode dans laquels nous prenons les assets de tous de notre scene pour pouvoir les tester
        public void OneTimeSetup()
        {
            boardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Board.prefab");
            caseTableauPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
            roquePrefab= AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
        }


        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadSceneAsync("Board");
            var waitForScene = new WaitForSceneLoaded("Board");
            yield return waitForScene;
            
            gameManagerGo = GameObject.Find("GameManager");
            playerControllerGo = GameObject.Find("Players");
            uIManagerGo = GameObject.Find("UIManager");
            boardGo = GameObject.Find("Board");

        }

            // Methode qui test si la piece est actuellement sur la piece 
        [Test]
        public void CaseContientPieceApresAvoirSetLaPiece()
        {
           //On ajoute aux variables que nous avons initialiser, les comporsantes des scripts que nous voulons tester.
            Case caseTableau = caseTableauPrefab.GetComponent<Case>();
            RoqueComportement roque = roquePrefab.GetComponent<RoqueComportement>();


            caseTableau.SetPieceDansLaCase(roque);
            //Assertation qui verifie si la case qui est dans la piece est la meme que celle de la variable roque que
            //nous avons initialiser           
            Assert.AreEqual(caseTableau.GetPieceDansLaCase(), roque);
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
        
        
        
        //
        // [UnityTest]    
        // public IEnumerator DeplacementRoque()
        // {
        //     GameObject caseGo00 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        //     GameObject caseGo01 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        //     GameObject caseGo02 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        //     GameObject caseGo03 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cube.prefab");
        //
        //     GameObject boardGo =
        //         AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
        //     BoardManager boardManager = boardGo.GetComponent<BoardManager>();
        //     
        //     GameObject roqueGo =
        //         AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess Rook White.prefab");
        //     RoqueComportement roqueComportement = roqueGo.GetComponent<RoqueComportement>(); 
        //     
        //     
        //     
        //     yield return null;
        //     
        //     
        //     
        //     
        //     //Mouvement possible pour la pieced de Roque
        //     Case caseTableau00 = caseGo00.GetComponent<Case>();
        //     caseTableau00.coordonneesDeCasePourBlanc = new Vector2Int(BoardManager.MAX_BOARD_SIZE, 0);//Mouvement vers toute la droite
        //     Case caseTableau01 = caseGo01.GetComponent<Case>();
        //     caseTableau01.coordonneesDeCasePourBlanc = new Vector2Int(-BoardManager.MAX_BOARD_SIZE,0);//Mouvement vers toute la gauche
        //     Case caseTableau02 = caseGo02.GetComponent<Case>();
        //     caseTableau02.coordonneesDeCasePourBlanc = new Vector2Int(0,BoardManager.MAX_BOARD_SIZE);//Mouvement vers tous en haut
        //     Case caseTableau03 = caseGo03.GetComponent<Case>();
        //     caseTableau03.coordonneesDeCasePourBlanc = new Vector2Int(0, -BoardManager.MAX_BOARD_SIZE);//Mouvement vers tous en bas
        //
        //
        //
        //
        //
        //
        //
        //
        // }
        
        
        

    }
