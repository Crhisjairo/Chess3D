using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

public class LoginSystem : MonoBehaviour
{
    public GameObject loginCanvas, registerCanvas;
    [SerializeField] private InputField loginEmailField, loginPasswordField;
    [SerializeField] private InputField registerEmailField, registerPasswordField1, registerPasswordField2, registerUsernameField;
    [SerializeField] private Button logInBtn, registerBtn, goToRegister, goToLogin, goToMainMenu;
    [SerializeField] private Text statusText;
    private string _avatarName;

    [SerializeField] private ToggleGroup _avatarsToggleGroup;
    
    string loginEmail = "";
    string loginPassword = "";
    
    string registerEmail = "";
    string registerPassword1 = "";
    string registerPassword2 = "";
    string registerUsername = "";

    private bool isWorking = false;
    private bool registrationCompleted = false;
    private bool isLoggedIn = false;
    
    //Logged-In user data
    private string userName = "";
    private string userEmail = "";

    private const string RootURL = "http://10.241.58.176/";

    public void ShowRegisterCanvas()
    {
        loginCanvas.SetActive(false);
        registerCanvas.SetActive(true);
    }

    public void ShowLoginCanvas()
    {
        loginCanvas.SetActive(true);
        registerCanvas.SetActive(false);
    }

    public void OnClickLogIn()
    {
        //On disable l'intéraction avec le boutton.
        logInBtn.interactable = false;
        goToRegister.interactable = false;
        goToMainMenu.interactable = false;
        
        loginEmail = loginEmailField.text;
        loginPassword = loginPasswordField.text;

        StartCoroutine(LoginEnumerator());
    }
    
    public void OnClickRegister()
    {
        //On disable l'intéraction avec le boutton.
        registerBtn.interactable = false;
        goToLogin.interactable = false;
        
        registerEmail = registerEmailField.text;
        registerUsername = registerUsernameField.text;
        registerPassword1 = registerPasswordField1.text;
        registerPassword2 = registerPasswordField2.text;

        //On recupère le checkbox
        Toggle toggle = _avatarsToggleGroup.ActiveToggles().FirstOrDefault();
        _avatarName = toggle.GetComponentInChildren<Image>().sprite.name;

        StartCoroutine(RegisterEnumerator());
    }
    
    /*
    * La méthode PlayGame(), je charge la scène ou le jeux va se dérouler.
    */
    public void StartGame()
    {
        SceneManager.LoadScene("SceneTest");
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

    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;

        WWWForm form = new WWWForm();
        form.AddField("email", loginEmail);
        form.AddField("password", loginPassword);

        using (UnityWebRequest www = UnityWebRequest.Post(RootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                statusText.text = www.error;
                statusText.color = Color.red;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    userName = dataChunks[1];
                    userEmail = dataChunks[2];
                    isLoggedIn = true;

                    ResetValues();
                    statusText.text = "Connection réussi!";
                    statusText.color = Color.green;

                    //TODO ICI ON CHANGE D'ÉCRAN
                }
                else
                {
                    statusText.text = responseText;
                    statusText.color = Color.red;
                }
                
                
            }
            
            //On habilite l'intéraction avec le boutton
            logInBtn.interactable = true;
            goToRegister.interactable = true;
            goToMainMenu.interactable = true;
        }

        isWorking = false;
    }

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;

        WWWForm form = new WWWForm();
        form.AddField("email",registerEmail);
        form.AddField("username",registerUsername);
        form.AddField("password1", registerPassword1);
        form.AddField("password2", registerPassword2);
        form.AddField("avatar", _avatarName); //Pour ajouter l'avatar

        using (UnityWebRequest www = UnityWebRequest.Post(RootURL + "register.php",form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                statusText.text = www.error;
                statusText.color = Color.red;
            }
            else
            {
                string reponseTexte = www.downloadHandler.text;

                if (reponseTexte.StartsWith("Success"))
                {
                    ResetValues();
                    registrationCompleted = true;
                    statusText.text = "Compte crée correctement";
                    statusText.color = Color.green;
                    
                    ShowLoginCanvas();
                }
                else
                {
                    statusText.text = reponseTexte;
                    statusText.color = Color.red;
                }
                
                
            }
            
            //On habilite l'intéraction avec le boutton
            registerBtn.interactable = true;
            goToLogin.interactable = true;
        }

        isWorking = true;
    }

    
    
    private void ResetValues()
    {
        statusText.text = "";
        loginEmail = "";
        loginPassword = "";
        registerEmail = "";
        registerPassword1 = "";
        registerPassword2 = "";
        registerUsername = "";
    }
}
