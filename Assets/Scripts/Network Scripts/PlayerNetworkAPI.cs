using UnityEngine;
using System.Collections;

public class PlayerNetworkAPI : MonoBehaviour {

	private PlayerHealth playerHealth;

	void Start() {
		playerHealth = GetComponent<PlayerHealth>();
	}

	[RPC]
	public void RemoveHealth(int amount) {
		playerHealth.RemoveHealth(amount);
	}
}
