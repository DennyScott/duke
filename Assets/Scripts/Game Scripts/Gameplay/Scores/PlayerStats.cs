using UnityEngine;
using System.Collections;

public class PlayerStats {

    public int Id;
    public int Kills;
    public int Deaths;
    public int DamageGiven;
    public int DamageTaken;

    public PlayerStats(int id) {
        this.Id = id;
    }
}
