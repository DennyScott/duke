using UnityEngine;
using System.Collections;

public class PlayerNetworkAPI : MonoBehaviour {

	private PlayerHealth playerHealth;
    private PhotonView _view;

	void Start() {
		playerHealth = GetComponent<PlayerHealth>();
	    _view = GetComponent<PhotonView>();
	}

	[RPC]
	public void RemoveHealth(int amount, int shooterId) {
		playerHealth.RemoveHealth(amount, shooterId);
	}

    [RPC]
    public void PlaySound(AudioClip soundToPlay, AudioSource source) {
        source.PlayOneShot(soundToPlay);
    }
}
