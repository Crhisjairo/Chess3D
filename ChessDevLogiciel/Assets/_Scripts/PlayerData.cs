using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string Id { get; private set; }
    public string Nom { get; private set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public int PartiesGagnes { get; set; }
    public int PartiesPerdues { get; set; }


    public PlayerData(string id, string nom, string username, string password, int partiesGagnes, int partiesPerdues)
    {
        Id = id;
        Nom = nom;
        Username = username;
        Password = password;
        PartiesGagnes = partiesGagnes;
        PartiesPerdues = partiesPerdues;
    }
}
