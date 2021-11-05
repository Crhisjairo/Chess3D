using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;

public class LoginSystem : MonoBehaviour
{
    public enum CurrentWindow
    {
        Login, Register
    }

    public CurrentWindow currentWindow = CurrentWindow.Login;


    string loginEmail = "";
    string loginPassword = "";
    string registerEmail = "";
    string registerPassword1 = "";
    string registerPassword2 = "";
    string registerUsername = "";
    private string errorMessage = "";


    private bool isWorking = false;
    private bool registrationCompleted = false;
    private bool isLoggedIn = false;
    
    
    
    //Logged-In user data
    private string userName = "";
    private string userEmail = "";


    private string rootURL = "http://10.241.58.176/";

    private void OnGUI()
    {
        if (!isLoggedIn)
        {
            if (currentWindow == CurrentWindow.Login)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 125, Screen.height / 2 - 115, 250, 230),
                    LoginWindow, "Login");
            }
            if (currentWindow == CurrentWindow.Register)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 125, Screen.height / 2 - 115, 250, 230),
                    RegisterWindow, "Register");
            }
        }
        GUI.Label(new Rect(5,5,500,25), "Status: " + (isLoggedIn ? "Logged-in Username: " + userName + "Email: "+ userEmail : 
            "Logged-out"));
        if (isLoggedIn)
        {
            if (GUI.Button(new Rect(5, 30, 100, 25), "Log-Out"))
            {
                isLoggedIn = false;
                userName = "";
                userEmail = "";
                currentWindow = CurrentWindow.Login;
            }
                
        }
    }
    private void LoginWindow(int id)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }

        if (registrationCompleted)
        {
            GUI.color = Color.green;
            GUILayout.Label("Registration Completed!!");
        }

        GUI.color = Color.white;
        GUILayout.Label("Email: ");
        loginEmail = GUILayout.TextField(loginEmail);
        GUILayout.Label("Password: ");
        loginPassword = GUILayout.PasswordField(loginPassword, '*');
        
        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(LoginEnumerator());
        }
        GUILayout.FlexibleSpace();
        
        GUILayout.Label("Do not have account ?");
        if (GUILayout.Button("Register", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Register;
        }
    }
    private void RegisterWindow(int id)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }

        GUI.color = Color.white;
        GUILayout.Label("Email: ");
        registerEmail = GUILayout.TextField(registerEmail, 254);
        GUILayout.Label("Username: ");
        registerUsername = GUILayout.TextField(registerUsername, 254);
        GUILayout.Label("Password: ");
        registerPassword1 = GUILayout.PasswordField(registerPassword1, '*',19);
        GUILayout.Label("Password Again: ");
        registerPassword2 = GUILayout.PasswordField(registerPassword2, '*',19);
        
        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(RegisterEnumerator());
        }
        
        GUILayout.FlexibleSpace();
        
        GUILayout.Label("Already have an account ?");
        if (GUILayout.Button("Login", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Login;
        }

    }

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email",registerEmail);
        form.AddField("username",registerUsername);
        form.AddField("password1", registerPassword1);
        form.AddField("password2", registerPassword2);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php",form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string reponseTexte = www.downloadHandler.text;

                if (reponseTexte.StartsWith("Success"))
                {
                    ResetValues();
                    registrationCompleted = true;
                    currentWindow = CurrentWindow.Login;
                }
                else
                {
                    errorMessage = reponseTexte;
                }
            }
        }

        isWorking = true;
    }

    private void ResetValues()
    {
        errorMessage = "";
        loginEmail = "";
        loginPassword = "";
        registerEmail = "";
        registerPassword1 = "";
        registerPassword2 = "";
        registerUsername = "";
    }

    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email", loginEmail);
        form.AddField("password", loginPassword);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
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
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }
   
}
