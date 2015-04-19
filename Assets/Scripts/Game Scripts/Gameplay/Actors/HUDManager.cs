using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	private PlayerHealth _playerHealth;

	public Text HealthHud;
	
	// Use this for initialization
	void Start () {
		_playerHealth = GetComponent<PlayerHealth>();
		InitalizeHealthHud();
	}

	void InitalizeHealthHud() {
		_playerHealth.OnHealthLost += UpdateHealthHud;
		_playerHealth.OnHealthGained += UpdateHealthHud;
		_playerHealth.OnResetHealth += UpdateHealthHud;
	}

	void UpdateHealthHud(GameObject g) {
		HealthHud.text = _playerHealth.PlayersHealth + "";
	}
}
