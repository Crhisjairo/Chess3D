using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*
     * La méthode PlayGame(), je charge la scène ou le jeux va se dérouler.
     */
    public void PlayGame()
    {
        SceneManager.LoadScene("Board");
    }
    /*
     * La méthode QuitGame() fait que lorsque le joueur clique sur le bouton quitter,
     * le jeu arrête et l'exécutable se ferme.
     */
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
