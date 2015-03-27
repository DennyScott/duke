using UnityEngine;
using System.Collections;

public class PlayerNetworkAPI : MonoBehaviour {

	private PlayerHealth playerHealth;

	void Start() {
		playerHealth = GetComponentInChildren<PlayerHealth>();
	}

	[RPC]
	public void RemoveHealth(int amount) {
		playerHealth.RemoveHealth(amount);
	}
}
