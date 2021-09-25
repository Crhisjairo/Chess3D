using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get;}

    [SerializeField] private Camera _cam;
    private void Awake()
    {
        //On évite avoir deux instance de cette même classe lors du Awake
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); //On destroy le gameObject qui contient ce script. Faire attention.
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangerCameraTo(Transform transform)
    {
        //Faut changer la camera par Cinemachine Virtual Camera
        _cam.transform.position = transform.position;
        _cam.transform.rotation = transform.rotation;
    }
}
