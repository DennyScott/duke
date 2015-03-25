using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
 
public class RandomMatchmaker : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
 
    void OnGUI()
    {
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
    	GameObject player = PhotonNetwork.Instantiate("FirstPerson", Vector3.zero, Quaternion.identity, 0);
    	player.GetComponent<CharacterController>().enabled = true;
    	player.GetComponent<FirstPersonController>().enabled = true;
    	player.GetComponent<AudioSource>().enabled = true;
    	player.GetComponentInChildren<Camera>().enabled = true;
    	player.GetComponentInChildren<AudioListener>().enabled = true;
    }
}