using UnityEngine;
using System.Collections;

public class PlayerHealth : Photon.MonoBehaviour {
	public int playersHealth = 100;

	public Animator damageAnimation;

	public void AddHealth(int heal) {
		playersHealth += heal;
	}

	public void ResetHealth() {
		playersHealth = 100;
	}

	public void RemoveHealth(int damage) {
		playersHealth -= damage;
		if(playersHealth <= 0) {
			OnDeath();
		}
		if(damageAnimation != null){
			damageAnimation.SetTrigger("Hit");
		}

	}

	void OnDeath() {
		ResetHealth();
		ResetPosition();
	}

	void ResetPosition() {
		transform.position = Vector3.zero;
	}
}
