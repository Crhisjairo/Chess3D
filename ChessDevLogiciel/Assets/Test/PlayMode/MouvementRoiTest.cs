﻿using UnityEngine;
using System.Collections;
using _Scripts;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
    public class MouvementRoiTest
    {
        //Initalisation de tous les variables que nous allons utiliser au long du code
        private GameObject boardPrefab;
        private GameObject caseTableauPrefab;
        private GameObject roiPrefab;
        
        
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
            roiPrefab= AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Pieces/Chess King White.prefab");
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
            RoiComportement roi = roiPrefab.GetComponent<RoiComportement>();


            caseTableau.SetPieceDansLaCase(roi);
            //Assertation qui verifie si la case qui est dans la piece est la meme que celle de la variable roque que
            //nous avons initialiser           
            Assert.AreEqual(caseTableau.GetPieceDansLaCase(), roi);
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
    }
