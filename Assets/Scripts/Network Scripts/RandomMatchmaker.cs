using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RandomMatchmaker : MonoBehaviour {

    public static System.Action<GameObject> JoinedRoom;
 
    // Use this for initialization
    void Start() {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnGUI() {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby() {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed() {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom() {
        var player = PhotonNetwork.Instantiate("FirstPlayer", Vector3.zero, Quaternion.identity, 0);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponentInChildren<Camera>().enabled = true;
        player.GetComponentInChildren<AudioListener>().enabled = true;
        foreach (var source in player.GetComponentsInChildren<AudioSource>()) {
            source.enabled = true;
        }
        player.GetComponent<PlayerWeaponManager>().enabled = true;
        player.GetComponent<PlayerHealth>().enabled = true;
        player.GetComponent<PlayerSpawning>().enabled = true;

        if (JoinedRoom != null) {
            JoinedRoom(player);
        }
    }
}