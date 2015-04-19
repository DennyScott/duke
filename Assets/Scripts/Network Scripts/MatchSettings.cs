using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchSettings : Photon.MonoBehaviour {

    public static int SpawnIndex;

    private static PhotonView _scenePhotonView;
    public Dictionary<int, PlayerStats> ActivePlayers = new Dictionary<int, PlayerStats>();  

    void Start() {
        _scenePhotonView = GetComponent<PhotonView>();
    }


    void OnPhotonPlayerConnected(PhotonPlayer player) {
        Debug.Log("Player Joined");
        ActivePlayers.Add(player.ID, new PlayerStats(player.ID));
        if (PhotonNetwork.isMasterClient) {
            _scenePhotonView.RPC("SetSpawnIndex", PhotonTargets.All, MatchSettings.SpawnIndex);
        }
    }

    void OnPhotonPlayerDisconnected(PhotonPlayer player) {
        Debug.Log("Player Left");
        ActivePlayers.Remove(player.ID);
    }

    public static void UpdateSpawnIndex() {
        MatchSettings.SpawnIndex = MatchSettings.SpawnIndex + 1 >= 3 ? 0 : MatchSettings.SpawnIndex + 1;  //If the index is higher then the list, it will reset back to 0
        _scenePhotonView.RPC("SetSpawnIndex", PhotonTargets.AllBuffered, MatchSettings.SpawnIndex);
    }

    public static void TriggerDeath() {
        _scenePhotonView.RPC("AddDeath", PhotonTargets.All, PhotonNetwork.player.ID);
    }

    [RPC]
    public void SetSpawnIndex(int passedSpawnIndex) {
        MatchSettings.SpawnIndex = passedSpawnIndex;
    }

    [RPC]
    public void AddKill(int playerId) {
//        ActivePlayers[playerId].Kills++;
    }

    [RPC]
    public void AddDeath(int playerId) {
//        ActivePlayers[playerId].Deaths++;
    }
}
