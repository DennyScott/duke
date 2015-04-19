using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchSettings : Photon.MonoBehaviour {

    #region Static Variables
    public static int SpawnIndex;
    private PhotonView _scenePhotonView;
    #endregion

    #region Public Variables
    public Dictionary<int, PlayerStats> ActivePlayers = new Dictionary<int, PlayerStats>();
    #endregion

    #region Standard Methods
    void Start() {
        _scenePhotonView = GetComponent<PhotonView>();
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// When a player Connects to the game
    /// </summary>
    /// <param name="player">The player who connected</param>
    private void OnPhotonPlayerConnected(PhotonPlayer player) {
        ActivePlayers.Add(player.ID, new PlayerStats(player.ID));
        if (PhotonNetwork.isMasterClient) {
            _scenePhotonView.RPC("SetSpawnIndex", PhotonTargets.All, MatchSettings.SpawnIndex);
        }
    }

    /// <summary>
    /// When a player disconnects from the game
    /// </summary>
    /// <param name="player">The player who disconnected</param>
    private void OnPhotonPlayerDisconnected(PhotonPlayer player) {
        ActivePlayers.Remove(player.ID);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets up events for the passed player object
    /// </summary>
    /// <param name="player">the player to set events up to</param>
    public void SetUpEvents(GameObject player) {
        player.GetComponent<PlayerHealth>().OnDeathAction += HandleOnDeath;
        player.GetComponent<PlayerSpawning>().OnSpawnAction += HandleOnSpawn;
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles when the user of this game spawns to update the spawn index
    /// </summary>
    private void HandleOnSpawn() {
        MatchSettings.SpawnIndex = MatchSettings.SpawnIndex + 1 >= 3 ? 0 : MatchSettings.SpawnIndex + 1;  //If the index is higher then the list, it will reset back to 0
        _scenePhotonView.RPC("SetSpawnIndex", PhotonTargets.AllBuffered, MatchSettings.SpawnIndex);
    }

    /// <summary>
    /// Handles when the player of this game dies to update the death counter, as well as the kill counter
    /// </summary>
    /// <param name="deadPlayer">The dead player gameobject</param>
    /// <param name="killerId">The Photon network ID of the killer</param>
    private void HandleOnDeath(GameObject deadPlayer, int killerId) {
        _scenePhotonView.RPC("AddDeath", PhotonTargets.All, PhotonNetwork.player.ID);
        _scenePhotonView.RPC("AddKill", PhotonTargets.All, killerId);
    }

    #endregion

    #region RPC Methods
    /// <summary>
    /// Sets the spawn index for all players
    /// </summary>
    /// <param name="passedSpawnIndex">The new spawn index</param>
    [RPC]
    public void SetSpawnIndex(int passedSpawnIndex) {
        MatchSettings.SpawnIndex = passedSpawnIndex;
    }

    /// <summary>
    /// Adds a kill to the player that killed this player
    /// </summary>
    /// <param name="playerId">The id of the killer</param>
    [RPC]
    public void AddKill(int playerId) {
        Debug.Log("Kill: " + playerId );
        //        ActivePlayers[playerId].Kills++;
    }

    /// <summary>
    /// Adds a death to the ID of the player dead
    /// </summary>
    /// <param name="playerId">The ID of the dead player</param>
    [RPC]
    public void AddDeath(int playerId) {
        Debug.Log("Death: " + playerId);
        //        ActivePlayers[playerId].Deaths++;
    }

    #endregion
}
