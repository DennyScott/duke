using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : Photon.MonoBehaviour {

	#region Public Variables
	public int PlayersHealth;
	public int MaxHealth = 100;
	#endregion

	#region Delegates and Events
	public System.Action<GameObject> OnDeathAction;
	public System.Action<GameObject> OnHealthGained;
	public System.Action<GameObject> OnHealthLost;
	public System.Action<GameObject> OnResetHealth;
	public System.Func<GameObject, bool> CanGainHealth;
	public System.Func<GameObject, bool> CanLoseHealth;
	#endregion

	#region Standard Methods
	void Start() {
		OnHealthLost += IsDead;
		PlayersHealth = MaxHealth;
	}
	#endregion


	#region Public Methods
	public void AddHealth(int heal) {
		TriggerHealthGained(heal);
	}

	public void ResetHealth() {
		TriggerResetHealth();
	}

	public void RemoveHealth(int damage) {
		TriggerHealthLost(damage);
	}
	#endregion

	#region Private Methods
	void IsDead(GameObject g) {
		if (PlayersHealth <= 0) {
			TriggerDeath();
		}
	}

	void OnDisable() {
		OnDeathAction = null;
	}
	#endregion

	#region Events Triggers
	void TriggerResetHealth() {
		PlayersHealth = MaxHealth;
		if(OnResetHealth != null) {
			OnResetHealth(gameObject);
		}
	}

	void TriggerHealthGained(int heal) {
		if (CanGainHealth != null && !CanGainHealth(gameObject)) {
			return;
		}

		PlayersHealth += heal;

		if (OnHealthGained != null) {
			OnHealthGained(gameObject);
		}
	}

	void TriggerHealthLost(int damage) {
		if (CanLoseHealth != null && !CanLoseHealth(gameObject)) {
			return;
		}

		PlayersHealth -= damage;
		if (OnHealthLost != null) {
			OnHealthLost(gameObject);
		}
	}

	void TriggerDeath() {
		if (OnDeathAction != null) {
			OnDeathAction(gameObject);
		}
		MatchSettings.TriggerDeath();
	}
	#endregion
}
