using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string Id { get; private set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public int PartiesGagnes { get; set; }
    public int PartiesPerdues { get; set; }


    public PlayerData(string id, string username, string email, string password, int partiesGagnes, int partiesPerdues, string avatarRessource)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        PartiesGagnes = partiesGagnes;
        PartiesPerdues = partiesPerdues;
    }
}
